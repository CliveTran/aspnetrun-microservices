
using catalog.api.Domain.Entities;
using MongoDB.Driver;

namespace catalog.api.Infrastructure.Persistance;
public interface ICatalogContext
{
    IMongoCollection<Product> Products { get; }
}
