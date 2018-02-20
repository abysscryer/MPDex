using MPDex.CacheRepository;
using MPDex.Models.Base;
using MPDex.Models.Domain;
using MPDex.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MPDex.Services
{
    public interface IBookCache
    {
        Task<BookCacheModel> IncreaseAsync(BookCacheModel book);
        Task<BookCacheModel> DecreaseAsync(BookCacheModel book);
        Task<IEnumerable<BookSummaryModel>> GetAsync(BookType bookType, short currencyId, short coinId, int size);
    }
}
