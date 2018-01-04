using Microsoft.EntityFrameworkCore;
using MPDex.Models;

namespace MPDex.Data
{
    public interface ICoinRepository : IRepository<Coin>
    { }
}
