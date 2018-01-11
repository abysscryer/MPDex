using System.Threading.Tasks;
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

        public BookService(IUnitOfWork unitOfWork, ILogger<BookService> logger, ILogger<Service<Book, BookCreateModel, BookUpdateModel, BookViewModel>> genericLogger)
            : base(unitOfWork, genericLogger)
        {
            this.repository = unitOfWork.CoinRepository;
            this.logger = logger;
        }

        public override Task<BookViewModel> AddAsync(BookCreateModel cModel)
        {
            return base.AddAsync(cModel);
        }
    }
}
