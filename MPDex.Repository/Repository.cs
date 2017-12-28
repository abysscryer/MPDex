using Microsoft.EntityFrameworkCore;
using MPDex.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace MPDex.Repository
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
        public Repository(MPDexContext context)
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
        public virtual void Save()
        {
            try
            {
                this.context.SaveChanges();
            }
            catch //(DbEntityValidationException e)
            {
                throw;
                //ThrowEnhancedValidationException(e);
            }
        }

        /// <summary>
        /// save async
        /// </summary>
        /// <returns></returns>
        public virtual Task SaveAsync()
        {
            try
            {
                return this.context.SaveChangesAsync();
            }
            catch //(DbEntityValidationException e)
            {
                throw;
                //ThrowEnhancedValidationException(e);
            }

            return Task.FromResult(0);
        }

        //protected virtual void ThrowEnhancedValidationException(DbEntityValidationException e)
        //{
        //    var errorMessages = e.EntityValidationErrors
        //            .SelectMany(x => x.ValidationErrors)
        //            .Select(x => x.ErrorMessage);

        //    var fullErrorMessage = string.Join("; ", errorMessages);
        //    var exceptionMessage = string.Concat(e.Message, " The validation errors are: ", fullErrorMessage);
        //    throw new DbEntityValidationException(exceptionMessage, e.EntityValidationErrors);
        //}

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
