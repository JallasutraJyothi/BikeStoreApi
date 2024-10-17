
using Bike_Store_App_WebApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bike_Store_App_WebApi.Data
{
    public class BikeStoreContext : DbContext
    {
        public BikeStoreContext(DbContextOptions<BikeStoreContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Inventory> Inventories { get; set; }

        public DbSet<SalesReport> SalesReports { get; set; }

        public DbSet<Review> Reviews { get; set; }

    }
}
