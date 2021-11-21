using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using transactions_api.Models;

namespace transactions_api.Interfaces
{
   public interface ICommentRepository
   {
      Task<List<Comment>> ListAsync();
      Task<List<Comment>> ListByTransactionAsync(Transaction transaction);
      Task<List<Comment>> ListByTickerAsync(string ticker);
      Task AddAsync(Comment comment);
      Task AddAsync(DateTime dateAdded, string text, Transaction transaction = null);
   }
}
