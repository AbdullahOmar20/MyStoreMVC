
using System.Text.Json;
using Core.Entities;
using Core.Entities.OrderAggregate;

namespace DBaccess.Data
{
    public class MyShopDbSeed
    {
        public static async Task SeedAsync(MyShopDbContext context)
        {
            if(!context.ProductBrand.Any())
            {
                var brandsdata = File.ReadAllText("../DBaccess/Data/SeedData/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsdata);
                context.ProductBrand.AddRange(brands);
            }
            if(!context.ProductType.Any())
            {
                var Typedata = File.ReadAllText("../DBaccess/Data/SeedData/types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(Typedata);
                context.ProductType.AddRange(Types);
            }
            if(!context.Products.Any())
            {
                var productssdata = File.ReadAllText("../DBaccess/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productssdata);
                context.Products.AddRange(products);
            }

            if(!context.DeliveryMethods.Any())
            {
                var deliveryData = File.ReadAllText("../DBaccess/Data/SeedData/delivery.json");
                var deliverys = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
                context.DeliveryMethods.AddRange(deliverys);
            }
            if(context.ChangeTracker.HasChanges())
            {
                await context.SaveChangesAsync();
            }
            
        }
    }
}