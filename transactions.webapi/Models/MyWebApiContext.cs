using Microsoft.EntityFrameworkCore;

namespace transactions_api.Models
{
    public class MyWebApiContext : DbContext
    {
        public MyWebApiContext(DbContextOptions<MyWebApiContext> options) : base(options) { }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<StocksLeft> StocksLeft { get; set; }
    }
}