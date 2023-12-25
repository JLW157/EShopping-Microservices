using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data;

public class CatalogContextSeed
{
    public static void SeedData(IMongoCollection<Product> productsCollection)
    {
        bool checkBrands = productsCollection.Find(brand => true).Any();
        string path = Path.Combine("Data", "SeedData", "brands.json");
        if (!checkBrands)
        {
            var productsData = File.ReadAllText(path);
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);
            if (products is not null)
            {
                foreach (var item in products)
                {
                    productsCollection.InsertOne(item);
                }
            }
        }
    }
}