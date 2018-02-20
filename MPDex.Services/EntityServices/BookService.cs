using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MPDex.Models.Base;
using MPDex.Models.Domain;
using MPDex.Models.ViewModels;
using MPDex.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPDex.Services
{
    public interface IBookService : IService<Book>
    {
        event EventHandler<BookChangedEventArgs> BookChanged;
        Task<IEnumerable<BookSummaryModel>> SumAsync(short currencyId, short coinId, BookType bookType);
        Task<BookViewModel> AddAsync(BookCreateModel cm);
        Task<IPagedList<BookViewModel>> GetPagedListAsync(int pageIndex, int pageSize, int indexFrom, int itemCount);
    }

    public class BookService : Service<Book>, IBookService
    {
        private readonly IRepository<Trade> tradeRepository;
        private readonly IRepository<Contract> contractRepository;
        private readonly IRepository<Balance> balanceRepository;
        private readonly IRepository<Fee> feeRepository;

        private int buy = ((int)BookType.Buy) - 1;
        private int sell = ((int)BookType.Sell) - 1;
        private int outgo = (int)BalanceType.Outgo;
        private int income = (int)BalanceType.Income;

        public event EventHandler<BookChangedEventArgs> BookChanged;

        public BookService(IUnitOfWork unitOfWork,
            ILogger<Service<Book>> logger)
            : base(unitOfWork, logger)
        {
            this.tradeRepository = unitOfWork.GetRepository<Trade>();
            this.contractRepository = unitOfWork.GetRepository<Contract>();
            this.balanceRepository = unitOfWork.GetRepository<Balance>();
            this.feeRepository = unitOfWork.GetRepository<Fee>();
        }

        /// <summary>
        /// event call back
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnBookChanged(BookChangedEventArgs e)
        {
            if (BookChanged != null)
            {
                BookChanged(this, e);
            }
        }

        public async Task<IPagedList<BookViewModel>> GetPagedListAsync(int pageIndex, int pageSize, int indexFrom, int itemCount)
        {
            return await base.GetPagedListAsync(x => new BookViewModel
            {
                Id = x.Id,
                CustomerId = x.Customer.Id,
                NickName = x.Customer.NickName,
                CoinId = x.Coin.Id,
                CoinName = x.Coin.Name,
                CurrencyId = x.CurrencyId,
                CurrencyName = x.Currency.Name,
                BookType = x.BookType,
                BookStatus = x.BookStatus,
                Price = x.Price,
                Amount = x.Amount,
                Stock = x.Stock,
                OrderCount = x.OrderCount,
                OnCreated = x.OnCreated,
                OnUpdated = x.OnUpdated,
                IPAddress = x.IPAddress,
                RowVersion = x.RowVersion
            }, pageIndex: pageIndex, pageSize: pageSize, indexFrom: indexFrom, itemCount: itemCount);
        }
        
        public async Task<IEnumerable<BookSummaryModel>> SumAsync(short currencyId, short coinId, BookType bookType)
        {

            IQueryable<Book> books = this.repository.Entitis.AsNoTracking();

            var query = books.Where(x => x.CoinId == 2
                && x.CurrencyId == 1
                && x.BookType == bookType
                && x.BookStatus == BookStatus.Placed)
            .GroupBy(x => new
            {
                x.Price,
                x.CoinId,
                x.CurrencyId
            })
            .Select(x => new BookSummaryModel
            {
                BookType = bookType,
                CurrencyId = x.Key.CurrencyId,
                CoinId = x.Key.CoinId,
                Price = x.Key.Price,
                Amount = x.Sum(s => s.Stock),
            });

            if (bookType == BookType.Buy)
                return await query.OrderBy(x => x.Price).ToListAsync();
            else
                return await query.OrderByDescending(x => x.Price).ToListAsync();

            #region FromSql

            //            var query = @"
            //SELECT Price, SUM(Stock) AS [Amount], COUNT(*) AS [Count]
            //	, (SELECT [Name] FROM dbo.Coin WHERE Id = {0}) AS [CurrencyName] 
            //	, (SELECT [Name] FROM dbo.Coin WHERE Id = {1}) AS [CoinName] 
            //FROM dbo.Book
            //WHERE CurrencyId = {0}
            //	AND CoinId = {1}
            //	AND BookType = {2}
            //	AND BookStatus = {3}
            //GROUP BY Price";


            //            var result = await repository.FromSql(query, currencyId, coinId, bookType, 1)
            //                .ToListAsync();

            //            if (bookType == BookType.Sell)
            //                return result.OrderByDescending(x => x.Price);
            //            else
            //                return result;

            #endregion
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
                cm.Id = Guid.NewGuid();
                var em = Mapper.Map<Book>(cm);
                var ok = await base.AddAsync(em);

                if (ok)
                {
                    IncreaseBookCache(em);
                    return await OrderAsync(em);
                }
                
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

                    DecreaseBooksCache(books, contract.Amount);
                    
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

                    DecreaseBooksCache(books, contract.Amount);

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

            var fee = await feeRepository.Get(predicate: x => x.CoinId == order.CoinId).SingleOrDefaultAsync();
            decimal feeAmount = 0;
            
            if (bookType == buy)
            {
                balanceAmount = contract.Price;

                if (balanceType == outgo)
                {
                    // 구매로 인한 지출 발생은 통화
                    balance = await balanceRepository.FindAsync(order.CustomerId, order.CurrencyId);

                    // 구매자의 지출은 가격
                    balanceAmount = contract.Price;

                    // 지출 처리
                    balanceAmount *= -1;

                    // 지출이 보유량 보다 많은 경우 논리오류
                    if (balance?.Amount + balanceAmount < 0)
                        throw new Exception($"Balance[{ order.CustomerId }, { order.CurrencyId }].Amount[{ balance?.Amount }] - Order[{order.Id}].Price[{contract.Amount}] update failed");
                }
                else
                {
                    // 구매로 인한 수입 발생은 코인
                    balance = await balanceRepository.FindAsync(order.CustomerId, order.CoinId);

                    // 구매자의 수입은 수량
                    balanceAmount = contract.Amount;

                    // 수수료 계산
                    feeAmount = (fee.Percent * balanceAmount) / 100;

                    // 수수료 차감
                    balanceAmount -= feeAmount;
                }
            }
            else
            {
                
                

                if (balanceType == outgo)
                {
                    // 판매로 인한 지출 발생은 코인
                    balance = await balanceRepository.FindAsync(order.CustomerId, order.CoinId);

                    // 판매자의 지출은 수량
                    balanceAmount = contract.Amount;

                    // 지출 처리
                    balanceAmount *= -1;

                    // 지출이 보유량보다 많은 경우 논리오류
                    if (balance?.Amount + balanceAmount < 0)
                        throw new Exception($"Balance[{ order.CustomerId }, { order.CoinId }].Amount[{ balance?.Amount }] - Order[{order.Id}].Amount[{contract.Amount}] update failed");
                }
                else
                {
                    // 판매로 인한 수입 발생은 통화
                    balance = await balanceRepository.FindAsync(order.CustomerId, order.CurrencyId);

                    // 판매자의 수입은 가격
                    balanceAmount = contract.Price;

                    // 수수료 계산
                    feeAmount = (fee.Percent * balanceAmount) / 100;

                    // 수수료 차감
                    balanceAmount -= feeAmount;
                }
            }
            
            balance.Amount += balanceAmount; 
            balance.Statements = new List<Statement>();

            var statement = new Statement
            {
                StatementId = order.Id,
                StatementType = (StatementType)bookType,
                BalanceType = Convert.ToBoolean(balanceType),
                BalanceAmount = balanceAmount,
                BeforeAmount = balance.Amount,
                AfterAmount = balance.Amount + balanceAmount,
                FeeAmount = feeAmount,
                CustomerId = order.CustomerId,
                CoinId = balance.CoinId
            };

            // 수입에는 연관 수수료 지정
            if ((BalanceType)balanceType == BalanceType.Income)
                statement.FeeId = fee.Id;
            
            balance.Statements.Add(statement);

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
            
            balances[sell, outgo] = await CreateBalance(contract, orders[sell], sell, outgo);
            balanceRepository.Update(balances[sell, outgo]);
            
            balances[buy, income] = await CreateBalance(contract, orders[buy], buy, income);
            balanceRepository.Update(balances[buy, income]);
            
            balances[sell, income] = await CreateBalance(contract, orders[sell], sell, income);
            balanceRepository.Update(balances[sell, income]);
            
            var effected = await unitOfWork.SaveChangesAsync();
            if (effected <= 0)
                throw new Exception($"Contract[{ contract.Id }].Orders.Customer.Balances, update failed");
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

        /// <summary>
        /// 예약 캐시 증가
        /// 소켓 호출
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        private void IncreaseBookCache(Book book)
        {
            try
            {
                var cm = Mapper.Map<BookCacheModel>(book);
                OnBookChanged(new BookChangedEventArgs(cm));
                //await bookCache.IncreaseAsync(cm);
            }
            catch (Exception ex)
            {
                // do not throw exception
                logger.LogError(ex, ex.Message, book);
            }
        }

        /// <summary>
        /// 예약 캐시 감소
        /// 소켓 호출
        /// </summary>
        /// <param name="books"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        private void DecreaseBooksCache(Book[] books, decimal amount)
        {
            try
            {   var bm = Mapper.Map<BookCacheModel>(books[buy]);
                bm.Amount = amount;
                OnBookChanged(new BookChangedEventArgs(bm, false));
                //await bookCache.DecreaseAsync(bm);

                var sm = Mapper.Map<BookCacheModel>(books[sell]);
                sm.Amount = amount;
                OnBookChanged(new BookChangedEventArgs(sm, false));
                //await bookCache.DecreaseAsync(sm);
            }
            catch (Exception ex)
            {
                // do not throw exception
                logger.LogError(ex, ex.Message, books, amount);
            }
        }
    }
}
