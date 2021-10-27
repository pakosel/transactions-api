using System;

namespace transactions_api.Dto
{
    public class TransactionReadDto
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
        public Nullable<decimal> Commision { get; set; }
        //public string CommisionCurrencySymbol { get; set; }
    }
}