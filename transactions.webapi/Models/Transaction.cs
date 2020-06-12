using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace transactions_api.Models
{
    public class Transaction
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Stock { get; set; }
        public string Market { get; set; }
        [Required]
        public string Operation { get; set; }
        [Required]
        public decimal Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string PriceCurrencySymbol { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public string AmountCurrencySymbol { get; set; }
        public Nullable<decimal> Commision { get; set; }

        [Column(TypeName = "jsonb")]
        public string Jsondata { get; set; }
    }
}