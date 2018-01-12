using MPDex.Models.Domain;
using MPDex.Models.ViewModels;

namespace MPDex.Services
{
    public interface IBookService : IService<Book, BookCreateModel, BookUpdateModel, BookViewModel>
    { }
}
