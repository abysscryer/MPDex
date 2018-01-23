using Microsoft.EntityFrameworkCore.Query;
using MPDex.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MPDex.Services
{
    public interface IService
    {
    }

    public interface IService<EM> : IService
        where EM : Entity
    {
        Task<IPagedList<VM>> GetPagedListAsync<VM>(Expression<Func<EM, VM>> selector,
            Expression<Func<EM, bool>> predicate = null,
            Func<IQueryable<EM>, IOrderedQueryable<EM>> orderBy = null,
            Func<IQueryable<EM>, IIncludableQueryable<EM, object>> include = null,
            int pageIndex = 0, int pageSize = 20, int indexFrom = 0, int itemCount = 0, bool disableTracking = true)
            where VM : class;

        Task<IEnumerable<VM>> Get<VM>(Expression<Func<EM, VM>> selector,
            Expression<Func<EM, bool>> predicate = null,
            Func<IQueryable<EM>, IOrderedQueryable<EM>> orderBy = null,
            Func<IQueryable<EM>, IIncludableQueryable<EM, object>> include = null
            , bool disableTracking = true) 
            where VM : class;

        Task<VM> FindAsync<VM>(params object[] keys) 
            where VM : class;

        Task<VM> AddAsync<CM, VM>(CM cm)
            where CM : class
            where VM : class;

        Task<VM> UpdateAsync<UM, VM>(UM um, params object[] keys)
            where UM : class
            where VM : class;
        
        Task<bool> RemoveAsync(params object[] keys);
    }
}
