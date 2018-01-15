using Microsoft.EntityFrameworkCore;
using MPDex.Models.Domain;

namespace MPDex.Repository
{
    public class ContractRepository : Repository<Contract>, IContractRepository
    {
        public ContractRepository(DbContext dbContext)
            : base(dbContext)
        { }
    }
}
