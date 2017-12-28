using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Domain
{
    public abstract class Entity<T>: IEntity<T>
    {
        public T Id { get; set; }
        object IEntity.Id
        {
            get { return this.Id; }
            set { this.Id = (T)Convert.ChangeType(value, typeof(T)); }
        }
    }
}
