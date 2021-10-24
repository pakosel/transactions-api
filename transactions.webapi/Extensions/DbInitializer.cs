using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using transactions_api.Models;

namespace transactions_api.Infrastructure
{
   public static class DbInitializer
   {
      public static void DatabaseUpdate(this IApplicationBuilder app)
      {
         using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
         {
            var dbContext = serviceScope.ServiceProvider.GetService<MyWebApiContext>();
            //if (!dbContext.Database.EnsureCreated())
            //   dbContext.Database.Migrate();
            //dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
         }
      }
   }
}