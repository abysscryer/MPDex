using Microsoft.EntityFrameworkCore;
using MPDex.Models.Domain;

namespace MPDex.Repository
{
    public class BalanceRepository : Repository<Balance>, IBalanceRepository
    {
        public BalanceRepository(DbContext dbContext)
            : base(dbContext)
        { }
    }
}
