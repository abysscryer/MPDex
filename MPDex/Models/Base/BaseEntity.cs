using System;

namespace MPDex.Models.Base
{
    /// <summary>
    /// base entity abstract class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    //public abstract class Entity<T>: IEntity<T>
    //{
    //    public T Id { get; set; }
    //    object IEntity.Id
    //    {
    //        get { return this.Id; }
    //        set { this.Id = (T)Convert.ChangeType(value, typeof(T)); }
    //    }
    //}

    public abstract class BaseEntity
    { }

    public abstract class BaseEntity<T> : BaseEntity, IEntity<T>
    {
        public virtual T Id { get; set; }
    }
}
