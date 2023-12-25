using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data;

public class TypeContextSeed
{
    public static void SeedData(IMongoCollection<ProductType> prodTypesCollection)
    {
        bool checkBrands = prodTypesCollection.Find(brand => true).Any();
        string path = Path.Combine("Data", "SeedData", "types.json");
        if (!checkBrands)
        {
            var prodTypesData = File.ReadAllText(path);
            var productTypes = JsonSerializer.Deserialize<List<ProductType>>(prodTypesData);
            if (productTypes is not null)
            {
                foreach (var item in productTypes)
                {
                    prodTypesCollection.InsertOne(item);
                }
            }
        }
    }
}