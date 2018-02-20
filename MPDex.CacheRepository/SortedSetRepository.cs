using Newtonsoft.Json;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPDex.CacheRepository
{
    public class SortedSetRepository<T> : ISortedSetRepository<T>
        where T : class
    {
        private readonly IDatabase db;

        public SortedSetRepository(IDatabase db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<T>> GetAsync(string key, long rank, Order order)
        {
            var items = new List<T>();

            foreach (var item in await db.SortedSetRangeByRankAsync(key, stop: rank, order: order))
            {
                items.Add(JsonConvert.DeserializeObject<T>(item));
            }

            return items;
        }

        public async Task<T> FindAsync(string key, double score)
        {
            var items = await db.SortedSetRangeByScoreAsync(key, score, score);
            return JsonConvert.DeserializeObject<T>(items.SingleOrDefault());
        }

        public async Task AddAsync(string key, double score, T entity)
        {
            await db.SortedSetAddAsync(key, JsonConvert.SerializeObject(entity), score);
        }

        public async Task UpdateAsync(string key, double score, T entity)
        {
            await this.RemoveAsync(key, score);
            await this.AddAsync(key, score, entity);
        }

        public async Task RemoveAsync(string key, double score)
        {
            await db.SortedSetRemoveRangeByScoreAsync(key, score, score);
        }
    }
}
