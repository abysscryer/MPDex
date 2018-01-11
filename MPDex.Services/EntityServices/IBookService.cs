using MPDex.Models.Domain;
using MPDex.Models.ViewModels;
using System.Threading.Tasks;

namespace MPDex.Services
{
    public interface IBookService : IService<Book, BookCreateModel, BookUpdateModel, BookViewModel>
    { }
}
