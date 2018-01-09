using Microsoft.EntityFrameworkCore;
using MPDex.Models.Domain;
using System.Threading.Tasks;
using System.Linq;

namespace MPDex.Repository
{
    public class CoinRepository : Repository<Coin>, ICoinRepository
    {
        public CoinRepository(DbContext dbContext) 
            : base(dbContext)
        { }

        public async Task<short> MaxAsync()
        {
            return await this.dbSet.DefaultIfEmpty().MaxAsync(c => c.Id);
        }
    }
}
