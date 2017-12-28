namespace MPDex.Domain.Base
{
    /// <summary>
    /// auditable model interface
    /// </summary>
    public interface IAuditable : IEditable
    {
        string IPAddress { get; set; }
        byte[] Version { get; set; }
    }
}
