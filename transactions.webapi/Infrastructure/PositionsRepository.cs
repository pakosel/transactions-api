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

        public Task<List<TransactionsGroup>> ListByTickerAsync(DateTime date, string ticker)
        {
            return _dbContext.Transactions
                .Where(t => t.Date <= date && t.Stock.ToLower() == ticker.ToLower())
                .GroupBy(t => t.Stock, (s, tt) => new TransactionsGroup()
                {
                    Stock = s,
                    Quantity = tt.Sum(x => x.Quantity)
                })
                .OrderBy(s => s.Stock)
                .ToListAsync();
        }
    }
}