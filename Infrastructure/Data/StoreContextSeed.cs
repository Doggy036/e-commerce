

using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {

        public static async Task SeedAsync(StoreContext context)
        {
            //檢查資料表中是否存在資料。
            //如果不存在，則從 XXXX.json 檔案中讀取資料，將其反序列化為 ProductXXXX 物件的列表，然後將這些物件添加到 DbContext 中以便保存到資料庫。

            if (!context.ProductBrands.Any())
            {
                var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                if (brands != null)
                {
                    context.ProductBrands.AddRange(brands);
                }
                else
                {
                    throw new Exception("Unable to load brands from JSON file.");
                }
            }

            if (!context.ProductTypes.Any())
            {
                var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                if (types != null)
                {
                    context.ProductTypes.AddRange(types);
                }
                else
                {
                    throw new Exception("Unable to load product types from JSON file.");
                }
            }

            if (!context.Products.Any())
            {
                var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products != null)
                {
                    context.Products.AddRange(products);
                }
                else
                {
                    throw new Exception("Unable to load products from JSON file.");
                }
            }

            if(context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();

            //這行程式碼會檢查 DbContext 是否有未保存的變更。ChangeTracker.HasChanges() 方法會檢查是否有新的、已刪除的或已變更的實體正在被追蹤，這些變更將在調用 SaveChanges() 或 SaveChangesAsync() 時被發送到資料庫




        }
    }
}