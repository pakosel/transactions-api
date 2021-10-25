using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace transactions_api.Models
{
   public class Profit
   {
      [Key]
      [Required]
      public int ProfitId { get; set; }
      public Transaction Buy { get; set; }
      [Required]
      public Transaction Sell { get; set; }
      [Required]
      public decimal BuyRatio { get; set; }
      [Required]
      public decimal SellRatio { get; set; }
      [Required]
      public decimal Amount { get; set; }
      public string AmountCurrencySymbol { get; set; }
   }
}