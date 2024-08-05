using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace DBaccess
{
    public class MyShopDbContext(DbContextOptions<MyShopDbContext> options) : DbContext(options)
    {        
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrand {get;set;}
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) => base.OnModelCreating(modelBuilder);
        
    }
}