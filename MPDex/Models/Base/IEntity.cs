namespace MPDex.Models.Base
{
    /// <summary>
    /// entity base interface
    /// </summary>
    //public interface IEntity
    //{
    //    object Id { get; set; }
    //}

    /// <summary>
    /// generic entity base interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    //public interface IEntity<T> : IEntity
    //{
    //    new T Id { get; set; }
    //}

    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
