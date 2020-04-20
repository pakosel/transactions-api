using Microsoft.EntityFrameworkCore;

namespace transactions_api.Models
{
    class MyWebApiContext : DbContext
    {
        public MyWebApiContext(DbContextOptions<MyWebApiContext> options) : base(options) {}

        public DbSet<Transactions> Transactions {get;set;}
    }
}