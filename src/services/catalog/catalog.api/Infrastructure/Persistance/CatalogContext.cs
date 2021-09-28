using catalog.api.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace catalog.api.Infrastructure.Persistance;
public class CatalogContext : ICatalogContext
{
    public CatalogContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

        Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
        CatalogContextSeeder.SeedData(Products);
    }
    public IMongoCollection<Product> Products { get; set; }
}
