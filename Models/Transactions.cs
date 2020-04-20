using System;
using System.ComponentModel.DataAnnotations.Schema;

public class Transactions
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Stock { get; set; }
    public string Market { get; set; }
    public string Operation { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public string PriceCurrencySymbol { get; set; }
    public decimal Amount { get; set; }
    public string AmountCurrencySymbol { get; set; }
    public decimal Commision { get; set; }
    
    [Column(TypeName = "jsonb")]
    public string Jsondata { get; set; }
}