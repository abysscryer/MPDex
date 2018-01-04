using System;

namespace MPDex.Models.Base
{
    /// <summary>
    /// Base Entity
    /// </summary>
    public abstract class BaseEntity
    { }

    /// <summary>
    /// Generic Base Entity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseEntity<T> : BaseEntity, IEntity<T>
    {
        public virtual T Id { get; set; }
    }
}
