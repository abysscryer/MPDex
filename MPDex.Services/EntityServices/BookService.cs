using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MPDex.Models.Base;
using MPDex.Models.Domain;
using MPDex.Models.ViewModels;
using MPDex.Repository;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace MPDex.Services
{
    public interface IBookService : IService<Book>
    {
        Task<BookViewModel> AddAsync(BookCreateModel cm);
    }

    public class BookService : Service<Book>, IBookService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IRepository<Trade> tradeRepository;
        private readonly IRepository<Contract> contractRepository;
        private readonly IRepository<Balance> balanceRepository;
        private readonly IRepository<Fee> feeRepository;

        private int buy = ((int)BookType.Buy)-1;
        private int sell = ((int)BookType.Sell)-1;
        private int outgo = (int)BalanceType.Outgo;
        private int income = (int)BalanceType.Income;

        public BookService(IUnitOfWork unitOfWork, 
                           ILogger<Service<Book>> logger, 
                           IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, logger)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.tradeRepository = unitOfWork.GetRepository<Trade>();
            this.contractRepository = unitOfWork.GetRepository<Contract>();
            this.balanceRepository = unitOfWork.GetRepository<Balance>();
            this.feeRepository = unitOfWork.GetRepository<Fee>();
        }

        /// <summary>
        /// 예약 추가
        /// </summary>
        /// <param name="cm"></param>
        /// <returns></returns>
        public async Task<BookViewModel> AddAsync(BookCreateModel cm)
        {
            try
            {
                cm.IPAddress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                cm.Id = Guid.NewGuid();
                var em = Mapper.Map<Book>(cm);
                var ok = await base.AddAsync(em);

                if (ok)
                    return await OrderAsync(em);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, cm);
                throw;
            }
            
            return null;
        }

        /// <summary>
        /// 주문 처리
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        private async Task<BookViewModel> OrderAsync(Book book)
        {
            Book em;
            BookViewModel vm;

            if (book.BookType == BookType.Buy)
                em = await BuyAsync(book);
            else
                em = await SellAsync(book);

            vm = Mapper.Map<BookViewModel>(em);

            return vm;
        }
        
        /// <summary>
        /// 매수 처리
        /// </summary>
        /// <param name="buyBook"></param>
        /// <param name="trade"></param>
        /// <returns></returns>
        private async Task<Book> BuyAsync(Book buyBook, Trade trade = null)
        {
            try
            {
                if (buyBook != null && buyBook.Stock != 0)
                {
                    // 판매물품 조회
                    var sellBook = await this.GetSellAsync(buyBook);
                    if (sellBook == null)
                        return buyBook;

                    // 트랜잭션 시작
                    repository.Context.Database.BeginTransaction();

                    // 예약 설정
                    var books = new Book[2];
                    books[buy] = buyBook;
                    books[sell] = sellBook;

                    // 예약 대기
                    await PendingBooks(books);

                    // 트레이드 생성
                    if (trade == null)
                        trade = await CreateTrade(books[buy]);

                    // 계약 체결
                    //trade.Contracts = new List<Contract>();
                    Contract contract = await CreateContract(trade, books);

                    //trade.Contracts.Add(contract);
                    // 잔고 변경
                    await UpdateBalances(contract);

                    // 예약 재고 계산 및 진행 처리
                    await UpdateBooks(books, contract);

                    // 트랜잭션 커밋
                    repository.Context.Database.CommitTransaction();

                    return await BuyAsync(buyBook, trade);
                }
            }
            catch (Exception ex)
            {
                // 트랜잭션 롤백
                repository.Context.Database.RollbackTransaction();
                logger.LogError(ex, ex.Message, trade);
                throw;
            }

            return buyBook;
        }
        
        /// <summary>
        /// 매도 처리
        /// </summary>
        /// <param name="sellBook"></param>
        /// <param name="trade"></param>
        /// <returns></returns>
        private async Task<Book> SellAsync(Book sellBook, Trade trade = null)
        {
            try
            {
                if (sellBook != null && sellBook.Stock != 0)
                {
                    // 판매물품 조회
                    var buyBook = await this.GetBuyAsync(sellBook);
                    if (buyBook == null)
                        return sellBook;

                    // 트랜잭션 시작
                    repository.Context.Database.BeginTransaction();

                    // 예약 설정
                    var books = new Book[2];
                    books[buy] = buyBook;
                    books[sell] = sellBook;

                    // 예약 대기
                    await PendingBooks(books);

                    // 트레이드 생성
                    if (trade == null)
                        trade = await CreateTrade(books[sell]);

                    // 계약 체결
                    //trade.Contracts = new List<Contract>();
                    Contract contract = await CreateContract(trade, books);

                    //trade.Contracts.Add(contract);
                    await UpdateBalances(contract);

                    // 예약 잔고 계산 및 진행
                    await UpdateBooks(books, contract);
                
                    // 트랜잭션 커밋
                    repository.Context.Database.CommitTransaction();

                    return await SellAsync(sellBook, trade);
                }
            }
            catch (Exception ex)
            {
                // 트랜잭션 롤백
                repository.Context.Database.RollbackTransaction();
                logger.LogError(ex, ex.Message, trade);
                throw;
            }

            return sellBook;
        }

        /// <summary>
        /// 하한 최고가 구매를 가져온다.
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        private async Task<Book> GetBuyAsync(Book book)
        {
            return await base.FirstAsync(
                predicate: x => x.BookStatus == BookStatus.Placed
                             && x.BookType == BookType.Buy
                             && x.CoinId == book.CoinId
                             && x.CurrencyId == book.CurrencyId
                             && x.Price >= book.Price,
                orderBy: x => x.OrderByDescending(b => b.Price)
                               .OrderBy(b => b.OnCreated)
            );
        }

        /// <summary>
        /// 상한 최저가 판매를 가져온다.
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        private async Task<Book> GetSellAsync(Book book)
        {
            return await base.FirstAsync(
                predicate: x => x.BookStatus == BookStatus.Placed
                             && x.BookType == BookType.Sell
                             && x.CoinId == book.CoinId
                             && x.CurrencyId == book.CurrencyId
                             && x.Price <= book.Price,
                orderBy: x => x.OrderBy(b => b.Price)
                               .OrderBy(b => b.OnCreated)
            );
        }

        /// <summary>
        /// 예약 팬딩
        /// </summary>
        /// <param name="books"></param>
        /// <returns></returns>
        private async Task PendingBooks(Book[] books)
        {   
            books[buy].BookStatus = BookStatus.Pending;
            books[sell].BookStatus = BookStatus.Pending;

            var effected = await base.UpdateAsync(books);
            //var effected = await unitOfWork.SaveChangesAsync();

            if (effected != 2)
                throw new Exception($"Book[{ books[buy].Id }, { books[sell].Id }].BookStatus[{ BookStatus.Pending }] change failed");
        }

        /// <summary>
        /// 거래 생성
        /// </summary>
        /// <param name="books"></param>
        /// <returns></returns>
        private async Task<Trade> CreateTrade(Book book)
        {
            Trade trade = new Trade()
            {
                CustomerId = book.CustomerId,
                CoinId = book.CoinId,
                CurrencyId = book.CurrencyId,
                TradeType = (TradeType)book.BookType,
                Price = book.Price,
                Amount = book.Stock,
            };

            tradeRepository.Add(trade);
            var effected = await unitOfWork.SaveChangesAsync();
            if (effected != 1)
                throw new Exception($"Create Trade by Book[{ book.Id }, { book.Id }] failed.");
            return trade;
        }

        /// <summary>
        /// 계약 생성
        /// </summary>
        /// <param name="tradeId"></param>
        /// <param name="books"></param>
        /// <returns></returns>
        private async Task<Contract> CreateContract(Trade trade, Book[] books)
        {
            var contract = new Contract();
            {
                contract.TradeId = trade.Id;

                // 계약 수량
                contract.Amount = books[sell].Stock >= books[buy].Stock ?
                    books[buy].Stock : books[sell].Stock;

                // 계약 가격
                if(trade.TradeType == TradeType.Buy)
                    contract.Price = books[buy].Price >= books[sell].Price ?
                        books[sell].Price : books[buy].Price;
                else
                    contract.Price = books[buy].Price >= books[sell].Price ?
                        books[buy].Price : books[sell].Price;

                contract.Orders = new List<Order>();
                contract.Orders.Add(new Order(books[buy]));
                contract.Orders.Add(new Order(books[sell]));

                contractRepository.Add(contract);
                var effected = await unitOfWork.SaveChangesAsync();
                if (effected <= 0)
                    throw new Exception($"Create Contract by Book[{ books[buy].Id }, { books[sell].Id }] failed");
            }

            return contract;
        }

        /// <summary>
        /// 밸런스 생성
        /// </summary>
        /// <param name="contract"></param>
        /// <param name="order"></param>
        /// <param name="bookType"></param>
        /// <param name="balanceType"></param>
        /// <returns></returns>
        private async Task<Balance> CreateBalance(Contract contract, Order order, int bookType, int balanceType)
        {
            Balance balance;
            decimal balanceAmount = 0;
            
            if (bookType == buy)
            {
                balance = await balanceRepository.FindAsync(order.CustomerId, order.CurrencyId);
                balanceAmount = contract.Price;

                if (balanceType == outgo)
                {
                    balanceAmount *= -1;

                    if (balance?.Amount + balanceAmount < 0)
                        throw new Exception($"Balance[{ order.CustomerId }, { order.CurrencyId }].Amount[{ balance?.Amount }] - Order[{order.Id}].Price[{contract.Amount}] update failed");
                }
            }
            else
            {
                balance = await balanceRepository.FindAsync(order.CustomerId, order.CoinId);
                balanceAmount = contract.Amount;

                if (balanceType == outgo)
                {
                    balanceAmount *= -1;

                    if (balance?.Amount + balanceAmount < 0)
                        throw new Exception($"Balance[{ order.CustomerId }, { order.CoinId }].Amount[{ balance?.Amount }] - Order[{order.Id}].Amount[{contract.Amount}] update failed");
                }
            }
            
            balance.Amount += balanceAmount; 
            balance.Statements = new List<Statement>();

            var fee = await feeRepository.Get(predicate: x => x.CoinId == order.CoinId).SingleOrDefaultAsync();

            balance.Statements.Add(new Statement
            {
                StatementId = order.Id,
                StatementType = (StatementType)bookType,
                BalanceType = Convert.ToBoolean(balanceType),
                BalanceAmount = contract.Amount,
                BeforeAmount = balance.Amount,
                AfterAmount = balance.Amount + balanceAmount,
                FeeId = fee.Id
            });

            return balance;
        }

        /// <summary>
        /// 밸런스 업데이트
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        private async Task UpdateBalances(Contract contract)
        {
            var orders = new Order[2];
            orders[buy] = contract.Orders.FirstOrDefault();
            orders[sell] = contract.Orders.LastOrDefault();

            var balances = new Balance[2, 2];
            balances[buy, outgo] = await CreateBalance(contract, orders[buy], buy, outgo);
            balanceRepository.Update(balances[buy, outgo]);
            await unitOfWork.SaveChangesAsync();

            balances[sell, outgo] = await CreateBalance(contract, orders[sell], sell, outgo);
            balanceRepository.Update(balances[sell, outgo]);
            await unitOfWork.SaveChangesAsync();

            balances[buy, income] = await CreateBalance(contract, orders[buy], buy, income);
            balanceRepository.Update(balances[buy, income]);
            await unitOfWork.SaveChangesAsync();

            balances[sell, income] = await CreateBalance(contract, orders[sell], sell, income);
            balanceRepository.Update(balances[sell, income]);
            await unitOfWork.SaveChangesAsync();

            //var effected = await unitOfWork.SaveChangesAsync();
            //if (effected <= 0)
            //    throw new Exception($"Contract[{ contract.Id }].Orders.Customer.Balances, update failed");
        }
        
        /// <summary>
        /// 예약 잔고 계산 및 진행
        /// </summary>
        /// <param name="books"></param>
        /// <param name="contract"></param>
        /// <returns></returns>
        private async Task UpdateBooks(Book[] books, Contract contract)
        {

            // 잔고 계산
            books[buy].Stock -= contract.Amount;
            books[sell].Stock -= contract.Amount;

            // 상태 변경
            books[buy].BookStatus = books[buy].Stock > 0 ?
                BookStatus.Placed : BookStatus.Completed;
            books[sell].BookStatus = books[sell].Stock > 0 ?
                BookStatus.Placed : BookStatus.Completed;

            // 주문 카운트 증가
            books[buy].OrderCount += 1;
            books[sell].OrderCount += 1;

            repository.Update(books);
            var effected = await unitOfWork.SaveChangesAsync();
            if (effected != 2)
                throw new Exception($"Book[{ books[buy].Id }, { books[sell].Id }].BookStatus[{ BookStatus.Placed }] change failed");
        }
    }
}
