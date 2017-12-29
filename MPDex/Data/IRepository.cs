using MPDex.Models.Base;
using System.Threading.Tasks;

namespace MPDex.Data
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
        int Save();

        /// <summary>
        /// save async
        /// </summary>
        /// <returns></returns>
        Task<int> SaveAsync();
    }
}
