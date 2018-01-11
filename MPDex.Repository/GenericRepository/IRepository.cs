using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MPDex.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MPDex.Repository
{
    public interface IRepository<EM> : IRepository
        where EM : Entity
    {
        DbSet<EM> Entitis { get; }
        IQueryable<VM> Get<VM>(Expression<Func<EM, VM>> selector,
            Expression<Func<EM, bool>> predicate = null,
            Func<IQueryable<EM>, IOrderedQueryable<EM>> orderBy = null,
            Func<IQueryable<EM>, IIncludableQueryable<EM, object>> include = null,
            int pageIndex = 0,
            int pageSize = 20,
            bool disableTracking = true) 
            where VM : class;
        IEnumerable<EM> GetBy(Expression<Func<EM, bool>> predicate);
        Task<EM> FindAsync(params object[] keys);
        void Add(EM em);
        void Update(EM em);
        void Remove(EM em);
    }

    public interface IRepository { }
}
