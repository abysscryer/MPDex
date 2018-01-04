using Microsoft.EntityFrameworkCore;
using MPDex.Models;

namespace MPDex.Data
{
    public class CoinRepository : Repository<Coin>, ICoinRepository
    {
        public CoinRepository(MPDexDbContext context) : base(context)
        { }
    }
}
