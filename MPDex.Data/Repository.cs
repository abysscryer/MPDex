using Microsoft.EntityFrameworkCore;
using MPDex.Models.Base;
using System.Threading.Tasks;

namespace MPDex.Data
{
    /// <summary>
    /// generic repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : ReadOnlyRepository<T>, IRepository<T>
        where T : class, IEntity
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="context"></param>
        public Repository(MPDexDbContext context)
            : base(context)
        { }

        /// <summary>
        /// create
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="createdBy"></param>
        public virtual void Create(T entity)
        {
            this.entities.Add(entity);
        }

        /// <summary>
        /// update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="modifiedBy"></param>
        public virtual void Update(T entity)
        {
            this.entities.Attach(entity);
            this.context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// find and delete
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(object id)
        {
            T entity = entities.Find(id);
            Delete(entity);
        }

        /// <summary>
        /// direct delete
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(T entity)
        {
            
            if (context.Entry(entity).State == EntityState.Detached)
            {
                entities.Attach(entity);
            }
            entities.Remove(entity);
        }

        /// <summary>
        /// save
        /// </summary>
        public virtual int Save()
        {
            return this.context.SaveChanges();
        }

        /// <summary>
        /// save async
        /// </summary>
        /// <returns></returns>
        public virtual async Task<int> SaveAsync()
        {
            return await this.context.SaveChangesAsync();
        }

        #region dispose
        
        /// <summary>
        /// dispose status
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                { }

                this.disposed = true;
                base.Dispose(disposing);
            }
        }

        /// <summary>
        /// finallizer
        /// </summary>
        ~Repository()
        {
            Dispose(false);
        }

        #endregion
    }
}
