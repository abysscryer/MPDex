using Microsoft.EntityFrameworkCore;
using MPDex.Models.Domain;

namespace MPDex.Repository
{
    public class FeeRepository : Repository<Fee>, IFeeRepository
    {
        public FeeRepository(DbContext dbContext)
            : base(dbContext)
        { }
    }
}
