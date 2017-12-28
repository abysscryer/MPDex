using System;
using System.Collections.Generic;
using System.Text;

namespace MPDex.Domain
{
    public interface IEntity
    {
        object Id { get; set; }
    }

    public interface IEntity<T> : IEntity
    {
        new T Id { get; set; }
    }
}
