using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using transactions_api.Models;
using transactions_api.Interfaces;

namespace transactions_api.Infrastructure
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly MyWebApiContext _dbContext;

        public TransactionsRepository(MyWebApiContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Transaction>> ListAsync()
        {
            return _dbContext.Transactions
                .OrderByDescending(t => t.Date)
                .ToListAsync();
        }

        public Task<List<Transaction>> ListByTickerAsync(string ticker)
        {
            return _dbContext.Transactions
                .Where(t => t.Stock == ticker)
                .OrderByDescending(t => t.Date)
                .ToListAsync();
        }

        public Task<Transaction> GetByIdAsync(int id)
        {
            return _dbContext.Transactions
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public Task AddAsync(Transaction transaction)
        {
            _dbContext.Transactions.Add(transaction);
            return _dbContext.SaveChangesAsync();
        }

        public Task UpdateAsync(Transaction transaction)
        {
            _dbContext.Entry(transaction).State = EntityState.Modified;
            return _dbContext.SaveChangesAsync();
        }
    }
}
