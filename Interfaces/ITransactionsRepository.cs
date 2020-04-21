using System.Collections.Generic;
using System.Threading.Tasks;
using transactions_api.Models;

namespace transactions_api.Interfaces
{
    public interface ITransactionsRepository
    {
        Task<List<Transaction>> ListAsync();
        Task<List<Transaction>> ListByTickerAsync(string ticker);
        Task<Transaction> GetByIdAsync(int id);
        Task AddAsync(Transaction transaction);
        Task UpdateAsync(Transaction transaction);
    }
}
