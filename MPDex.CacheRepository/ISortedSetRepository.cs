using System.Collections.Generic;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace MPDex.CacheRepository
{
    public interface ISortedSetRepository<T>
        where T : class
    {
        Task<IEnumerable<T>> GetAsync(string key, long rank, Order order);

        Task<T> FindAsync(string key, double score);

        Task AddAsync(string key, double score, T entity);

        Task UpdateAsync(string key, double score, T entity);

        Task RemoveAsync(string key, double score);

    }
}
