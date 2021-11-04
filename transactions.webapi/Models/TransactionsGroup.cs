namespace transactions_api.Models
{
    public class TransactionsGroup
    {
        public string Stock { get; set; }
        public decimal Quantity { get; set; }
        public decimal AvgPrice { get; set; }
        public string PriceCurrencySymbol { get; set; }
        public decimal SumCommision { get; set; }
    }
}