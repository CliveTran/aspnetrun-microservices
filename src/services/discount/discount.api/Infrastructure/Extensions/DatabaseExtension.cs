using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;
using System.Threading;

namespace discount.api.Infrastructure.Extensions;
public static class DatabaseExtension
{
    public static WebApplication MigrateData<T>(this WebApplication app, int? retry = 0)
    {
        // When using scoped services, if you're not creating a scope or within an existing scope - the service becomes a singleton.
        // See: https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection-guidelines

        using (IServiceScope scope = app.Services.CreateScope())
        {
            var configuration = app.Services.GetRequiredService<IConfiguration>();
            var logger = app.Services.GetRequiredService<ILogger<T>>();

            try
            {
                ExecuteMigration(configuration);
                logger.LogInformation("Migrate success.");
            }

            catch (NpgsqlException ex)
            {
                if (retry.HasValue && retry.Value < 50)
                {
                    logger.LogError($"Migrate error: {ex}.");
                    retry++;
                    Thread.Sleep(2000);
                    MigrateData<T>(app, retry);
                }
            }

            return app;
        }
    }

    private static void ExecuteMigration(IConfiguration configuration)
    {
        using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        connection.Open();
        using var command = new NpgsqlCommand
        {
            Connection = connection
        };

        command.CommandText = "DROP TABLE IF EXISTS Coupon";
        command.ExecuteNonQuery();

        command.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, 
                                                                ProductName VARCHAR(24) NOT NULL,
                                                                Description TEXT,
                                                                Amount INT)";
        command.ExecuteNonQuery();


        command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('IPhone X', 'IPhone Discount', 150);";
        command.ExecuteNonQuery();

        command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Samsung 10', 'Samsung Discount', 100);";
        command.ExecuteNonQuery();
    }
}
