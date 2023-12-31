using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories;

public class ProductRepository : IProductRepository, IBrandRepository, ITypesRepository
{
    private readonly ICatalogContext _context;

    public ProductRepository(ICatalogContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetProducts() =>
        await _context
            .Products
            .Find(product => true).ToListAsync();

    public async Task<Product> GetProduct(string id) =>
        await _context.Products
            .Find(product => product.Id == id)
            .FirstOrDefaultAsync();

    public async Task<IEnumerable<Product>> GetProductsByName(string name)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter
            .Eq(product => product.Name, name);

        return await _context.Products
            .Find(filter)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByBrand(string brandName)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter
            .Eq(product => product.Brands.Name, brandName);

        return await _context.Products
            .Find(filter)
            .ToListAsync();
    }

    public async Task<IEnumerable<ProductBrand>> GetAllBrands() =>
        await _context.Brands.Find(brand => true).ToListAsync();

    public async Task<IEnumerable<ProductType>> GetAllTypes() => await _context.Types.Find(brand => true).ToListAsync();

    public async Task<Product> CreateProduct(Product product)
    {
        await _context.Products.InsertOneAsync(product);
        return product;
    }

    public async Task<bool> UpdateProduct(Product productToUpdate)
    {
        var updateResult = await _context.Products
            .ReplaceOneAsync(product => product.Id == productToUpdate.Id, productToUpdate);

        return updateResult is { IsAcknowledged: true, ModifiedCount: > 0 };
    }

    public async Task<bool> DeleteProduct(string id)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(product => product.Id, id);
        DeleteResult deleteResult = await _context
            .Products
            .DeleteOneAsync(filter);

        return deleteResult is { IsAcknowledged: true, DeletedCount: > 0 };
    }
}