namespace MPDex.Models.ViewModels
{
    public class OrderCreateModel
    {
        public long Id { get; set; }

        public virtual BookViewModel Book { get; set; }
        
        public virtual long ContractId  { get; set; }
    }
}
