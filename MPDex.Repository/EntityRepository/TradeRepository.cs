using Microsoft.EntityFrameworkCore;
using MPDex.Models.Domain;

namespace MPDex.Repository
{
    public class TradeRepository : Repository<Trade>, ITradeRepository
    {
        public TradeRepository(DbContext dbContext)
            : base(dbContext)
        { }
    }
}
