using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using transactions_api.Models;
using transactions_api.Interfaces;
using System;

namespace transactions_api.Infrastructure
{
   public class CommentRepository : ICommentRepository
   {
      private readonly MyWebApiContext _dbContext;

      public CommentRepository(MyWebApiContext dbContext)
      {
         _dbContext = dbContext;
      }

      public Task<List<Comment>> ListAsync()
      {
         return _dbContext.Comment
            .OrderByDescending(c => c.DateAdded)
            .ToListAsync();
      }

      public Task<List<Comment>> ListByTransactionIdAsync(int transactionId)
      {
         return _dbContext.Comment
            .Where(c => c.TransactionId == transactionId)
            .ToListAsync();
      }

      public Task<List<Comment>> ListByTickerAsync(string ticker)
      {
         return _dbContext.Comment
            .Where(t => t.Transaction.Stock == ticker)
            .ToListAsync();
      }

      public Task AddAsync(Comment comment)
      {
         _dbContext.Comment.Add(comment);
         return _dbContext.SaveChangesAsync();
      }

      public Task AddAsync(DateTime dateAdded, string text, Transaction transaction = null)
      {
         _dbContext.Comment.Add(new Comment()
         {
            DateAdded = DateTime.UtcNow,
            Text = text,
            Transaction = transaction
         });
         return _dbContext.SaveChangesAsync();
      }
   }
}
