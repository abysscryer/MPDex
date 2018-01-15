using Microsoft.EntityFrameworkCore;
using MPDex.Models.Domain;

namespace MPDex.Repository
{
    public class StatementRepository : Repository<Statement>, IStatementRepository
    {
        public StatementRepository(DbContext dbContext)
            : base(dbContext)
        { }
    }
}
