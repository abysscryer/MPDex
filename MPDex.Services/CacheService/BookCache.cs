using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using MPDex.CacheRepository;
using MPDex.Models.Domain;
using MPDex.Models.ViewModels;
using MPDex.Models.Base;
using StackExchange.Redis;

namespace MPDex.Services
{
    public class BookCache : IBookCache
    {
        private readonly IDatabase cache;
        
        public BookCache(IRedisConnectionFactory factory)
        {
            this.cache = factory.Connection().GetDatabase();
        }

        public async Task<BookCacheModel> IncreaseAsync(BookCacheModel book)
        {
            book.Amount = await cache.SortedSetIncrementAsync(book.Key, book.Price.ToString("N8"), (double)book.Amount)
                .ContinueWith(x => (decimal)x.Result);
            
            return book;
        }

        public async Task<BookCacheModel> DecreaseAsync(BookCacheModel book)
        {
            book.Amount = await cache.SortedSetDecrementAsync(book.Key, book.Price.ToString("N8"), (double)book.Amount)
                .ContinueWith(x => (decimal)x.Result);

            return book;
        }

        public async Task<IEnumerable<BookSummaryModel>> GetAsync(BookType bookType, short currencyId, short coinId, int size=10)
        {
            var sets = await cache.SortedSetRangeByScoreWithScoresAsync(GetKey(bookType, currencyId, coinId), 0.00000001);

            var models = new List<BookSummaryModel>();

            if (bookType == BookType.Buy)
            {
                models = sets.OrderByDescending(x => decimal.Parse(x.Element))
                    .Take(size)
                    .Select(x => new BookSummaryModel
                    {
                        BookType = bookType,
                        CurrencyId = currencyId,
                        CoinId = coinId,
                        Price = decimal.Parse(x.Element),
                        Amount = (decimal)x.Score
                    }).ToList();
            }
            else
            {
                models = sets
                    .OrderBy(x => decimal.Parse(x.Element))
                    .Take(size)
                    .Select(x => new BookSummaryModel
                    {
                        BookType = bookType,
                        CurrencyId = currencyId,
                        CoinId = coinId,
                        Price = decimal.Parse(x.Element),
                        Amount = (decimal)x.Score
                    }).ToList();
            }
            
            return models;    
        }
        
        private string GetKey(params object[] elements)
        {
            return string.Join("|", "Book", string.Join(":", elements));
        }

        /// <summary>
        /// 8자리 절사
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private double GetFormatValue(decimal x)
        {
            return double.Parse(x.ToString("N8"));
        }
    }
}
