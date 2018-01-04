namespace MPDex.Models.Base
{
    /// <summary>
    /// Generic BaseEntity interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
