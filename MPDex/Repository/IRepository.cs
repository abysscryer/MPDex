using System;
using System.Text;
using System.Threading.Tasks;
using MPDex.Domain;

namespace MPDex.Repository
{
    /// <summary>
    /// generic repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> : IReadOnlyRepository<T>
        where T : class, IEntity
    {
        /// <summary>
        /// create
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="createdBy"></param>
        void Create(T entity);
        
        /// <summary>
        /// update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="modifiedBy"></param>
        void Update(T entity);

        /// <summary>
        /// find and delete
        /// </summary>
        /// <param name="id"></param>
        void Delete(object id);

        /// <summary>
        /// direct delete
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// save
        /// </summary>
        void Save();

        /// <summary>
        /// save async
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();
    }
}
