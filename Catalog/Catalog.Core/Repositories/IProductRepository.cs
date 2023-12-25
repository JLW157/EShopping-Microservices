using Catalog.Core.Entities;

namespace Catalog.Core.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProducts();
    Task<Product> GetProduct(string id);
    Task<IEnumerable<Product>> GetProductsByName(string name);
    Task<IEnumerable<Product>> GetProductsByBrand(string brandName);
    Task<IEnumerable<ProductBrand>> GetAllBrands();
    Task<IEnumerable<ProductType>> GetAllTypes();
    Task<Product> CreateProduct(Product product);
    Task<bool> UpdateProduct(Product productToUpdate);
    Task<bool> DeleteProduct(string id);
}