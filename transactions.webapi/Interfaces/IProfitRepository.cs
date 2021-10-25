using System.Collections.Generic;
using System.Threading.Tasks;
using transactions_api.Models;

namespace transactions_api.Interfaces
{
   public interface IProfitRepository
   {
      Task<List<Profit>> ListAsync();
      Task AddAsync(Transaction transactionBuy, Transaction transactionSell, decimal buyRatio, decimal sellRatio, decimal amount, string amountCurrency = null);
   }
}
