using System.Collections.Generic;
using System.Threading.Tasks;
using transactions_api.Models;

namespace transactions_api.Interfaces
{
   public interface IStocksLeftRepository
   {
      Task<List<StocksLeft>> ListAsync();
      Task AddAsync(int transactionId, decimal quantity);
      //Task UpdateAsync(StocksLeft stocksLeft, decimal quantity);
   }
}
