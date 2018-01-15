namespace MPDex.Models.Base
{
    /// <summary>
    /// status start with 1
    /// </summary>
    public enum BookStatus : byte {
        Pending = 0, // just created
        Placed, // balance updated
        Completed, // book matched
        Canceled, // book canceled
        Expired // book expired
    }
}
