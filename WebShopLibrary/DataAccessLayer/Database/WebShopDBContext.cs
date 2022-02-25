using Microsoft.EntityFrameworkCore;
using System;
using WebShopLibrary.DataAccessLayer.Database.Entities.Products;
using WebShopLibrary.DataAccessLayer.Database.Entities.Products.Games;
using WebShopLibrary.DataAccessLayer.Database.Entities.Products.Monitors;
using WebShopLibrary.DataAccessLayer.Database.Entities.Transactions;
using WebShopLibrary.DataAccessLayer.Database.Entities.Users;

namespace WebShopLibrary.DataAccessLayer.Database
{
    public class WebShopDBContext : DbContext
    {
        public WebShopDBContext() { }

        public WebShopDBContext(DbContextOptions<WebShopDBContext> contextOptions) : base(contextOptions) { }

        public DbSet<Game> Game { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Purchase> Purchase { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Monitor> Monitor { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "TestCategory",
                    Description = "TestCategoryDescription"
                },
                new Category
                {
                    Id = 2,
                    Name = "TestCategory2",
                    Description = "TestCategoryDescription2"
                }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product 
                {
                    Id = 1,
                    Name = "TestProductName",
                    Price = 99.95m,
                    Description = "TestProductDescription2",
                    Stock = 22
                },
                new Product
                {
                    Id = 2,
                    Name = "TestProductName2",
                    Price = 599.95m,
                    Description = "TestProductDescription",
                    Stock = 10
                });

            modelBuilder.Entity<Game>()
                .HasIndex(u => u.ProductId)
                    .IsUnique();

            modelBuilder.Entity<Game>().HasData(
                new Game
                {
                    Id = 1,
                    Publisher = "FirstTestGamePublisher",
                    PublishedYear = 1991,
                    Language = "FirstTestLanguage",
                    Genre = "FirstTestGenre",
                    ProductId = 1,
                    CategoryId = 2
                },
                new Game
                {
                    Id = 2,
                    Publisher = "SecondTestGamePublisher",
                    PublishedYear = 1991,
                    Language = "SecondTestLanguage",
                    Genre = "SecondTestGenre",
                    ProductId = 2,
                    CategoryId = 1
                });

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "TestUsername",
                    Password = "TestPassword",
                    Email = "Test@Test.test",
                    UserType = "Admin"
                },
                new User
                {
                    Id = 2,
                    Username = "TestUsername2",
                    Password = "TestPassword2",
                    Email = "Test@Test.test",
                    UserType = "Client"
                });

            modelBuilder.Entity<Purchase>().HasData(
                new Purchase
                {
                    Id = 1,
                    PurchaseDate = DateTime.Now,
                    ProductId = 1,
                    UserId = 1
                },
                new Purchase
                {
                    Id = 2,
                    PurchaseDate = DateTime.Now,
                    ProductId = 2,
                    UserId = 2
                });

            modelBuilder.Entity<Monitor>().HasData(
                new Monitor
                {
                    Id = 1,
                    Brand = "Sony",
                    ReleaseYear = 2017,
                    Size = 15,
                    CategoryId = 3,
                    ProductId = 1
                }
                );
        }
    }
}
