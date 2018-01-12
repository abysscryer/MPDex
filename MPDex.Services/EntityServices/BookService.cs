using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MPDex.Models.Domain;
using MPDex.Models.ViewModels;
using MPDex.Repository;

namespace MPDex.Services
{
    public class BookService : Service<Book, BookCreateModel, BookUpdateModel, BookViewModel>, IBookService
    {
        private readonly ICoinRepository repository;
        private readonly ILogger<BookService> logger;
        private readonly IHttpContextAccessor httpContextAccessor;

        public BookService(IUnitOfWork unitOfWork, 
                           ILogger<BookService> logger, 
                           ILogger<Service<Book, BookCreateModel, BookUpdateModel, BookViewModel>> genericLogger, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, genericLogger)
        {
            this.repository = unitOfWork.CoinRepository;
            this.logger = logger;
            this.httpContextAccessor = httpContextAccessor;
        }

        public override async Task<BookViewModel> AddAsync(BookCreateModel cm)
        {
            BookViewModel vm = null;
            try
            {
                cm.IPAddress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                cm.Id = Guid.NewGuid();
                var em = Mapper.Map<Book>(cm);
                var ok = await base.AddAsync(em);

                if(ok)
                    vm = await base.GetSingle(x => new BookViewModel
                    {
                        NickName = x.Customer.NickName,
                        CoinName = x.Coin.Name,
                        OrderType = x.OrderType,
                        Price = x.Price,
                        Amount = x.Amount,
                        Stock = x.Stock
                    }, predicate: x => x.Id == cm.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, cm, vm);
                throw;
            }
            
            return vm;
        }
    }
}
