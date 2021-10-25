using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using transactions_api.Models;
using transactions_api.Interfaces;

namespace transactions_api.Infrastructure
{
   public class StocksLeftRepository : IStocksLeftRepository
   {
      private readonly MyWebApiContext _dbContext;

      public StocksLeftRepository(MyWebApiContext dbContext)
      {
         _dbContext = dbContext;
      }

      public Task<List<StocksLeft>> ListRemainingAsync(string ticker)
      {
         return _dbContext.Transactions
             .OrderBy(t => t.Date)
             .Where(t => t.Stock == ticker)
             .Join(_dbContext.StocksLeft, t => t.TransactionId, sl => sl.TransactionId, (t, sl) => sl)
             .Where(sl => sl.Quantity > 0)
             .ToListAsync();
      }

      public Task AddAsync(int transactionId, decimal quantity)
      {
         _dbContext.StocksLeft.Add(new StocksLeft() { TransactionId = transactionId, Quantity = quantity });
         return _dbContext.SaveChangesAsync();
      }

      public Task UpdateAsync(StocksLeft stocksLeft, decimal quantity)
      {
         _dbContext.StocksLeft.FirstOrDefault(sl => sl.StocksLeftId == stocksLeft.StocksLeftId).Quantity = quantity;
         return _dbContext.SaveChangesAsync();
      }

   }
}
