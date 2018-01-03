using MPDex.Models;
using System.Threading.Tasks;

namespace MPDex.Data
{
    public interface ICoinRepository : IRepository<Coin>
    {
        //Coin GetById(short id);

        //Task<Coin> GetByIdAsync(short id);
    }
}
