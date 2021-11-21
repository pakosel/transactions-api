using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace transactions_api.Models
{
   public class Comment
   {
      [Key]
      [Required]
      public int CommentId { get; set; }
      [Required]
      public DateTime DateAdded { get; set; }
      public int TransactionId { get; set; }
      public Transaction Transaction { get; set; }
      [Required]
      public string Text { get; set; }
   }
}