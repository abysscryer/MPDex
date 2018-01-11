using Microsoft.EntityFrameworkCore;
using MPDex.Models.Domain;
using System.Threading.Tasks;
using System.Linq;
using MPDex.Models.ViewModels;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace MPDex.Repository
{
    public class CoinRepository : Repository<Coin>, ICoinRepository
    {
        public CoinRepository(DbContext dbContext) 
            : base(dbContext)
        { }

        public async Task<short> MaxAsync()
        {
            return await this.dbSet.DefaultIfEmpty().MaxAsync(c => c.Id);
        }
    }
}
