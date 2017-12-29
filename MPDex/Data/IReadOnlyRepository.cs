using MPDex.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MPDex.Data
{
    /// <summary>
    /// read only generic repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IReadOnlyRepository<T>: IDisposable
        where T : class, IEntity
    {
        /// <summary>
        /// get all entities
        /// </summary>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        IEnumerable<T> GetAll(
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null);

        /// <summary>
        /// get all entites async
        /// </summary>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync(
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null);

        /// <summary>
        /// get entities
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null);

        /// <summary>
        /// get entities async
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null);

        /// <summary>
        /// get single entity
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        T GetOne(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null);

        /// <summary>
        /// get single entity async
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        Task<T> GetOneAsync(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null);

        /// <summary>
        /// get first entity
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        T GetFirst(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null);

        /// <summary>
        /// get first entity async
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        Task<T> GetFirstAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null);

        /// <summary>
        /// find entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(object id);

        /// <summary>
        /// find entity async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(object id);

        /// <summary>
        /// count entities
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        int GetCount(Expression<Func<T, bool>> filter = null);

        /// <summary>
        /// count entities async
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<int> GetCountAsync(Expression<Func<T, bool>> filter = null);

        /// <summary>
        /// is exists entity
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        bool GetExists(Expression<Func<T, bool>> filter = null);

        /// <summary>
        /// is exists async
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<bool> GetExistsAsync(Expression<Func<T, bool>> filter = null);
    }
}
