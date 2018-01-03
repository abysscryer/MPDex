using MPDex.Models;
using MPDex.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MPDex.Data
{
    public class CoinRepository : Repository<Coin>, ICoinRepository
    {
        public CoinRepository(MPDexDbContext context) : base(context)
        { }

        //public Coin GetById(short id)
        //{
        //    return this._dbSet.Find(id);   
        //}

        //public async Task<Coin> GetByIdAsync(short id)
        //{
        //    return await this._dbSet.FindAsync(id);
        //}
    }
}
