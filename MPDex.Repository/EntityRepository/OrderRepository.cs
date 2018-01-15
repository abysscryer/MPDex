using Microsoft.EntityFrameworkCore;
using MPDex.Models.Domain;

namespace MPDex.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(DbContext dbContext)
            : base(dbContext)
        { }
    }
}
