using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using transactions_api.Models;
using transactions_api.Interfaces;

namespace transactions_api.Infrastructure
{
   public class HoldingsRepository : IHoldingsRepository
   {
      private readonly MyWebApiContext _dbContext;

      public HoldingsRepository(MyWebApiContext dbContext)
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

      public Task<List<TransactionsGroup>> ListGroupByTickerAsync(string ticker)
      {
         return _dbContext.Transactions
            .Join(_dbContext.StocksLeft, transaction => transaction.TransactionId, sl => sl.TransactionId,
               (transaction, sl) => new {transaction, sl})
             .Where(t => t.transaction.Operation == "BUY")
             .Where(t => string.IsNullOrEmpty(ticker) ||
                        (!ticker.Contains("*") && t.transaction.Stock.ToLower() == ticker.ToLower()) ||
                        (ticker.Contains("*") && t.transaction.Stock.ToLower().StartsWith(ticker.ToLower().Replace("*", ""))))
             .Where(t => _dbContext.Profit.Any(p => p.Buy.TransactionId == t.transaction.TransactionId) == false ||
                        _dbContext.Profit.Where(p => p.Buy.TransactionId == t.transaction.TransactionId).Sum(p => p.QtySold) < t.transaction.Quantity)
             .GroupBy(t => t.transaction.Stock, (s, tt) => new TransactionsGroup()
             {
                Stock = s,
                Quantity = tt.Sum(x => x.sl.Quantity),
                AvgPrice = tt.Sum(x => x.transaction.Price * x.sl.Quantity) / tt.Sum(x => x.sl.Quantity),
                PriceCurrencySymbol = tt.Max(x => x.transaction.PriceCurrencySymbol),
                SumCommision = tt.Sum(x => x.transaction.Commision ?? 0)
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
            .Join(_dbContext.StocksLeft, transaction => transaction.TransactionId, sl => sl.TransactionId,
               (transaction, sl) => new {transaction, sl})
            .Where(t => t.sl.Quantity > 0)
            .Where(t => t.transaction.Operation == "BUY")
            .Where(t => string.IsNullOrEmpty(ticker) ||
                        (!ticker.Contains("*") && t.transaction.Stock.ToLower() == ticker.ToLower()) ||
                        (ticker.Contains("*") &&
                         t.transaction.Stock.ToLower().StartsWith(ticker.ToLower().Replace("*", ""))))
            .Where(t => _dbContext.Profit.Any(p => p.Buy.TransactionId == t.transaction.TransactionId) == false ||
                        _dbContext.Profit.Where(p => p.Buy.TransactionId == t.transaction.TransactionId)
                           .Sum(p => p.QtySold) < t.transaction.Quantity)
            .OrderBy(t => t.transaction.TransactionId)
            .Select(t => t.transaction)
            .ToListAsync();
      }
   }
}