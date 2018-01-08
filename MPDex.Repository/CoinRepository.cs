using Microsoft.EntityFrameworkCore;
using MPDex.Models.Domain;
using System.Threading.Tasks;

namespace MPDex.Repository
{
    public class CoinRepository : Repository<Coin>, ICoinRepository
    {
        public CoinRepository(DbContext dbContext) 
            : base(dbContext)
        { }

        public async Task<short> MaxAsync()
        {
            var coin = await this.dbSet.MaxAsync();
            return coin == null ? (short)0 : coin.Id;
        }
    }
}
