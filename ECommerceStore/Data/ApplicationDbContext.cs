using ECommerceStore.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceStore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Product> Products  { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(18, 2);

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Dell Laptop",
                    Description = "Powerful laptop for work and productivity",
                    Price = 15000,
                    Stock = 10,
                    CategoryId = 1,
                    ImageUrl = "/images/laptop.jpg",
                    CreatedAt = DateTime.UtcNow.Date
                },
                new Product
                {
                    Id = 2,
                    Name = "Cotton T-Shirt",
                    Description = "Comfortable cotton t-shirt",
                    Price = 150,
                    Stock = 50,
                    CategoryId = 2,
                    ImageUrl = "/images/tshirt.jpg",
                    CreatedAt = DateTime.UtcNow.Date
                },
                new Product
                {
                    Id = 3,
                    Name = "Programming Book",
                    Description = "Learn programming from scratch",
                    Price = 200,
                    Stock = 30,
                    CategoryId = 3,
                    ImageUrl = "/images/book.jpg",
                    CreatedAt = DateTime.UtcNow.Date
                }
                );
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics", Description = "Electronic devices and tech products" },
                new Category { Id = 2, Name = "Clothing", Description = "Men's and women's clothing" },
                new Category { Id = 3, Name = "Books", Description = "Books and magazines" });
        }
    }
}
