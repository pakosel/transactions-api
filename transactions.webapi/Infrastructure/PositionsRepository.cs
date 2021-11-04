using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using transactions_api.Models;
using transactions_api.Interfaces;

namespace transactions_api.Infrastructure
{
   public class PositionsRepository : IPositionsRepository
   {
      private readonly MyWebApiContext _dbContext;

      public PositionsRepository(MyWebApiContext dbContext)
      {
         _dbContext = dbContext;
      }

      public Task<List<TransactionsGroup>> ListByDateAsync(DateTime date)
      {
         return _dbContext.Transactions
             .Where(t => t.Date <= date)
             .GroupBy(t => t.Stock, (s, tt) => new TransactionsGroup()
             {
                Stock = s,
                Quantity = tt.Sum(x => x.Quantity)
             })
             .Where(tg => tg.Quantity != 0)
             .OrderBy(s => s.Stock)
             .ToListAsync();
      }

      public Task<List<TransactionsGroup>> ListGroupByTickerAsync()
      {
         return _dbContext.Transactions
             .Where(t => t.Operation == "BUY")
             .GroupBy(t => t.Stock, (s, tt) => new TransactionsGroup()
             {
                Stock = s,
                Quantity = tt.Sum(x => x.Quantity),
                AvgPrice = tt.Sum(x => x.Price * x.Quantity) / tt.Sum(x => x.Quantity),
                PriceCurrencySymbol = tt.Max(x => x.PriceCurrencySymbol),
                SumCommision = tt.Sum(x => x.Commision ?? 0)
             })
             .OrderBy(s => s.Stock)
             .ToListAsync();
      }

      public Task<List<Transaction>> ListOpenAsync()
      {
         return ListOpenByTickerAsync("");
      }

      public Task<List<Transaction>> ListOpenByTickerAsync(string ticker)
      {
         return _dbContext.Transactions
            .Where(t => t.Operation == "BUY")
            .Where(t => string.IsNullOrEmpty(ticker) || 
                        (!ticker.Contains("*") && t.Stock.ToLower() == ticker.ToLower()) || 
                        (ticker.Contains("*") && t.Stock.ToLower().StartsWith(ticker.ToLower().Replace("*",""))))
            .Where(t => _dbContext.Profit.Any(p => p.Buy.TransactionId == t.TransactionId) == false ||
                        _dbContext.Profit.Where(p => p.Buy.TransactionId == t.TransactionId).Sum(p => p.QtySold) < t.Quantity)
            .OrderBy(t => t.TransactionId)
            .ToListAsync();
      }
   }
}