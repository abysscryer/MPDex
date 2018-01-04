namespace MPDex.Models.Base
{
    /// <summary>
    /// auditable model interface
    /// </summary>
    public interface IAuditable : IEditable
    {
        string IPAddress { get; set; }
        byte[] RowVersion { get; set; }
    }
}
