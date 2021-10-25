using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using transactions_api.Models;
using transactions_api.Interfaces;
using System;

namespace transactions_api.Infrastructure
{
   public class ProfitRepository : IProfitRepository
   {
      private readonly MyWebApiContext _dbContext;

      public ProfitRepository(MyWebApiContext dbContext)
      {
         _dbContext = dbContext;
      }

      public Task<List<Profit>> ListAsync()
      {
         return _dbContext.Profit
             .ToListAsync();
      }

      public Task AddAsync(Transaction transactionBuy, Transaction transactionSell, decimal buyRatio, decimal sellRatio, decimal amount, string amountCurrency = null)
      {
         _dbContext.Profit.Add(new Profit()
         {
            Buy = transactionBuy,
            Sell = transactionSell,
            BuyRatio = Math.Round(buyRatio, 2),
            SellRatio = Math.Round(sellRatio, 2),
            Amount = Math.Round(amount, 2),
            AmountCurrencySymbol = amountCurrency
         });
         return _dbContext.SaveChangesAsync();
      }
   }
}
