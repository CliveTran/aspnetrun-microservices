using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;
using System.Threading;

namespace discount.grpc.Infrastructure.Extensions;
public static class DatabaseExtension
{
    public static WebApplication MigrateData<TContext>(this WebApplication app, int? retry = 0)
    {
        var services = app.Services;
        var configuration = services.GetRequiredService<IConfiguration>();
        var logger = services.GetRequiredService<ILogger<TContext>>();

        try
        {
            logger.LogInformation("Migrating...");
            ExecuteMigration(configuration);
        }

        catch (NpgsqlException ex)
        {
            if (retry.HasValue && retry.Value < 50)
            {
                logger.LogInformation("Migrating failed, retry...");
                retry++;
                Thread.Sleep(2000);
                MigrateData<TContext>(app, retry);
            }
            logger.LogError(ex.Message);
        }

        return app;
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
