using Microsoft.EntityFrameworkCore;
using MPDex.Models.Domain;
using System.Linq;
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
            return await this.dbSet.DefaultIfEmpty().MaxAsync(c => c.Id);
        }
    }
}
