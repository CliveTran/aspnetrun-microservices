﻿
using Npgsql;

namespace discount.api.Infrastructure.Extensions;
public static class DatabaseExtension
{
    public static WebApplication MigrateData(this WebApplication app, int? retry = 0)
    {
        var services = app.Services;
        var configuration = services.GetRequiredService<IConfiguration>();

        try
        {
            ExecuteMigration(configuration);
        }

        catch (NpgsqlException ex)
        {
            if (retry.HasValue && retry.Value < 50)
            {
                retry++;
                Thread.Sleep(2000);
                MigrateData(app, retry);
            }
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
