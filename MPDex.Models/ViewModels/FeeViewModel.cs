namespace MPDex.Models.ViewModels
{
    public class FeeViewModel
    {
        public short Id { get; set; }

        public decimal Percent { get; set; }

        public virtual string CoinName { get; set; }
    }
}
