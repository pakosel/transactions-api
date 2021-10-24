using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace transactions_api.Models
{
   public enum TransactionOperation
   {
      BUY,
      SELL,
      DIVIDEND
   }
   
   public enum TransactionMarket
   {
      GPW,
      NYSE
   }

   public class DictMarket
   {
      [Key]
      [Required]
      public int Id { get; set; }
      [Required]
      public string Name { get; set; }
   }
}