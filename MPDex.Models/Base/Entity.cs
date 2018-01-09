using System;

namespace MPDex.Models.Base
{
    /// <summary>
    /// Base Entity
    /// </summary>
    public abstract class Entity : IEntity
    { }

    /// <summary>
    /// Generic Base Entity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Entity<T> : Entity, IEntity<T>
    {
        public virtual T Id { get; set; }
    }
}
