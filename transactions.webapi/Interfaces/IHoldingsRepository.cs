using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using transactions_api.Models;

namespace transactions_api.Interfaces
{
   public interface IHoldingsRepository
   {
      Task<List<TransactionsGroup>> ListByDateAsync(DateTime date);
      Task<List<TransactionsGroup>> ListGroupByTickerAsync();
      Task<List<Transaction>> ListOpenAsync();
      Task<List<Transaction>> ListOpenByTickerAsync(string ticker);
   }
}
