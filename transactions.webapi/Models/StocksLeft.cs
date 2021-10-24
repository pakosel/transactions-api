using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace transactions_api.Models
{
    public class StocksLeft
    {
        [Key]
        [Required]
        public int StocksLeftId { get; set; }
        [Required]
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }
        [Required]
        public decimal Quantity { get; set; }
    }
}