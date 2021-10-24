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

      public Task<List<StocksLeft>> ListAsync()
      {
         return _dbContext.StocksLeft
             .ToListAsync();
      }

      public Task AddAsync(int transactionId, decimal quantity)
      {
         _dbContext.StocksLeft.Add(new StocksLeft() { TransactionId = transactionId, Quantity = quantity });
         return _dbContext.SaveChangesAsync();
      }
   }
}
