using Microsoft.EntityFrameworkCore;
using MPDex.Models.Base;
using System.Threading.Tasks;

namespace MPDex.Data
{
    /// <summary>
    /// generic repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class _Repository<C, T> : ReadOnlyRepository<C, T>, _IRepository<T>
        //where T : class, IEntity
        where T : BaseEntity
        where C : DbContext
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="context"></param>
        //public Repository(C context, T entity)
        //    : base(context, entity)
        //{}
        public _Repository(C context)
            : base(context)
        { }
        /// <summary>
        /// create
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="createdBy"></param>
        public virtual void Create(T entity)
        {
            this.Entities.Add(entity);
        }

        /// <summary>
        /// update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="modifiedBy"></param>
        public virtual void Update(T entity)
        {
            this.Entities.Attach(entity);
            this.Context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// find and delete
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(object id)
        {
            T entity = this.Entities.Find(id);
            Delete(entity);
        }

        /// <summary>
        /// direct delete
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(T entity)
        {
            
            if (this.Context.Entry(entity).State == EntityState.Detached)
            {
                this.Entities.Attach(entity);
            }
            this.Entities.Remove(entity);
        }

        /// <summary>
        /// save
        /// </summary>
        public virtual int Save()
        {
            return this.Context.SaveChanges();
        }

        /// <summary>
        /// save async
        /// </summary>
        /// <returns></returns>
        public virtual async Task<int> SaveAsync()
        {
            return await this.Context.SaveChangesAsync();
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
        ~_Repository()
        {
            Dispose(false);
        }

        #endregion
    }
}
