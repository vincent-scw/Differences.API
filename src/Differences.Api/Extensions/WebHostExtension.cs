using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Differences.Api.Extensions
{
    public static class WebHostExtension
    {
        public static IWebHost MigrateDatabase(this IWebHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<DataAccess.DifferencesDbContext>();
                
                dbContext.Database.Migrate();
            }

            return webHost;
        }
    }
}
