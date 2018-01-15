namespace MPDex.Models.ViewModels
{
    public class FeeCreateModel
    {
        public short Id { get; set; }

        public decimal Percent { get; set; }
        
        public virtual short CoinId { get; set; }
    }
}
