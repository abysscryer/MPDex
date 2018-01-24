using MPDex.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace MPDex.Models.ViewModels
{
    public class BookCreateModel
    {
        public Guid Id { get; set; }

        [Required]
        public BookType BookType { get; set; }
        
        [Range(0.0000001, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0.0000001, double.MaxValue)]
        public decimal Amount { get; set; }

        [Range(0.0000001, double.MaxValue)]
        public decimal Stock { get; set; }
        
        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public short CoinId { get; set; }

        [Required]
        public short CurrencyId { get; set; }

        public string IPAddress { get; set; }
    }

    public class BookOrderModel
    {
        [Range(0.0000001, double.MaxValue)]
        public decimal Amount { get; set; }

        [Range(0.0000001, double.MaxValue)]
        public decimal Stock { get; set; }

        [Range(1, 255)]
        public byte OrderCount { get; set; }

        [Required]
        public byte[] RowVersion { get; set; }
    }

    public class BookStatusModel
    {
        public BookStatus bookStatus { get; set; }

        [Required]
        public byte[] RowVersion { get; set; }
    }

    public class BookViewModel
    {
        public Guid Id { get; set; }

        public BookType BookType { get; set; }

        public BookStatus BookStatus { get; set; }

        public decimal Price { get; set; }

        public decimal Amount { get; set; }

        public decimal Stock { get; set; }
        
        public byte OrderCount { get; set; }

        public Guid CustomerId { get; set; }
        
        public string NickName { get; set; }

        public short CoinId { get; set; }

        public short CurrencyId { get; set; }

        public string CoinName { get; set; }

        public string IPAddress { get; set; }

        public DateTime OnCreated { get; set; }

        public DateTime? OnUpdated { get; set; }

        public byte[] RowVersion { get; set; }
    }
}