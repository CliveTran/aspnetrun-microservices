
using catalog.api.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace catalog.api.Infrastructure.Repositories;
public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProducts();

    Task<Product> GetProduct(string id);

    Task<IEnumerable<Product>> GetProductByName(string name);

    Task<IEnumerable<Product>> GetProductByCategory(string categoryName);

    Task CreateProduct(Product product);

    Task<bool> UpdateProduct(Product product);

    Task<bool> DeleteProduct(string id);
}
