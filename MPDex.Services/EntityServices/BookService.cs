using Microsoft.Extensions.Logging;
using MPDex.Models.Domain;
using MPDex.Repository;

namespace MPDex.Services.EntityServices
{
    public class BookService : Service<Book>, IBookService
    {
        private readonly ICoinRepository repository;
        private readonly ILogger<BookService> logger;

        public BookService(IUnitOfWork unitOfWork, ILogger<BookService> logger, ILogger<Service<Book>> genericLogger)
            : base(unitOfWork, genericLogger)
        {
            this.repository = unitOfWork.CoinRepository;
            this.logger = logger;
        }
    }
}
