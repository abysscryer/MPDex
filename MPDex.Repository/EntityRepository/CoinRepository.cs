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
            return await this.dbSet.MaxAsync(x => x.Id);
        }
    }
}
