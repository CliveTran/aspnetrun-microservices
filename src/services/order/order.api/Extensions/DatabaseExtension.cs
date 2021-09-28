using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace order.api.Extensions
{
    public static class DatabaseExtension
    {
        public static WebApplication MigrateData<TContext>(
            this WebApplication app,
            Action<TContext, IServiceProvider> seeder,
            int? retry = 0) where TContext : DbContext
        {
            int retryForAvailibility = retry.GetValueOrDefault();

            using (IServiceScope scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetRequiredService<TContext>();

                try
                {
                    logger.LogInformation("Migrating {DbContextName}...", typeof(TContext));
                    context.Database.Migrate();
                    seeder(context, services);
                    logger.LogInformation("Migrated successfully {DbContextName}.", typeof(TContext));
                }

                catch (SqlException ex)
                {
                    throw;
                }
            }

                return app;
        }
    }
}
