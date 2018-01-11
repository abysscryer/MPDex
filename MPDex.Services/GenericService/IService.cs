using Microsoft.EntityFrameworkCore.Query;
using MPDex.Models.Base;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MPDex.Services
{
    public interface IService
    {
    }

    public interface IService<EM, CM, UM, VM> : IService
        where EM : Entity
        where CM : class
        where UM : class
        where VM : class
    {
        Task<IPagedList<VM>> GetPagedListAsync(Expression<Func<EM, VM>> selector,
            Expression<Func<EM, bool>> predicate = null,
            Func<IQueryable<EM>, IOrderedQueryable<EM>> orderBy = null,
            Func<IQueryable<EM>, IIncludableQueryable<EM, object>> include = null,
            int pageIndex = 0, int pageSize = 20, int indexFrom = 0, int itemCount = 0, bool disableTracking = true);

        Task<VM> FindAsync(params object[] keyValues);

        Task<VM> AddAsync(CM cModel);

        Task<VM> UpdateAsync(UM createModel, params object[] keyValues);

        Task<bool> RemoveAsync(params object[] keyValues);
    }
}
