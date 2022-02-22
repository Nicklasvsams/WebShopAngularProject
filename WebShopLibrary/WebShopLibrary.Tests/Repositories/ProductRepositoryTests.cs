using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebShopLibrary.DataAccessLayer.Database;
using WebShopLibrary.DataAccessLayer.Database.Entities.Products;
using WebShopLibrary.DataAccessLayer.Repositories.ProductRepositories;
using Xunit;

namespace WebShopLibrary.Tests.Repositories
{
    public class ProductRepositoryTests
    {
        private readonly DbContextOptions<WebShopDBContext> _options;
        private readonly WebShopDBContext _context;
        private readonly ProductRepository _productRepository;

        public ProductRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<WebShopDBContext>()
                .UseInMemoryDatabase(databaseName: "WebShopProduct")
                .Options;
            _context = new WebShopDBContext(_options);
            _productRepository = new ProductRepository(_context);
        }

        [Fact]
        public async void SelectAllProducts_ShouldReturnListOfProducts_WhenProductsExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            _context.Product.Add(ProductList(1, 2)[0]);
            _context.Product.Add(ProductList(1, 2)[1]);

            await _context.SaveChangesAsync();

            // Act
            var result = await _productRepository.SelectAllProducts();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Product>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void SelectAllProducts_ShouldReturnEmptyListOfProducts_WhenNoProductsExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _productRepository.SelectAllProducts();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Product>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void DeleteProductById_ShouldReturnDeletedProduct_WhenProductExists()
        {
            // Arrange
            int productId = 1;

            await _context.Database.EnsureDeletedAsync();

            _context.Product.Add(ProductList(1, 2)[0]);
            _context.Product.Add(ProductList(1, 2)[1]);

            await _context.SaveChangesAsync();

            // Act
            var result = await _productRepository.DeleteProductById(productId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(productId, result.Id);
        }

        [Fact]
        public async void DeleteProductById_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _productRepository.DeleteProductById(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void SelectProductById_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            int productId = 1;

            await _context.Database.EnsureDeletedAsync();

            _context.Product.Add(Product(productId));

            await _context.SaveChangesAsync();

            // Act
            var result = await _productRepository.SelectProductById(productId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(productId, result.Id);
            Assert.Equal("TestName1", result.Name);
            Assert.Equal(19.95m, result.Price);
            Assert.Equal("TestDescription", result.Description);
            Assert.Equal(1, result.Stock);
        }

        [Fact]
        public async void SelectProductById_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            int productId = 1;

            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _productRepository.SelectProductById(productId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void CreateProduct_ShouldAddIdAndReturnProduct_WhenProductIsSuccessfullyCreated()
        {
            // Arrange
            int productId = 1;

            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _productRepository.InsertNewProduct(Product());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(productId, result.Id);
            Assert.Equal("TestName1", result.Name);
            Assert.Equal(19.95m, result.Price);
            Assert.Equal("TestDescription", result.Description);
            Assert.Equal(1, result.Stock);
        }

        [Fact]
        public async void CreateProduct_ShouldFailToAddProduct_WhenProductWithSameIdAlreadyExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            Product product = new Product
            {
                Id = 1,
                Name = "Test",
                Price = 1m,
                Description = "Test",
                Stock = 1
            };

            _context.Add(product);

            await _context.SaveChangesAsync();

            // Act
            async Task action() => await _productRepository.InsertNewProduct(product);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async void UpdateExistingProduct_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            int productId = 1;

            await _context.Database.EnsureDeletedAsync();

            _context.Product.Add(Product(productId));

            await _context.SaveChangesAsync();

            Product product = new Product
            {
                Id = 1,
                Name = "Test123",
                Price = 12m,
                Description = "Test123",
                Stock = 12
            };

            // Act
            var result = await _productRepository.UpdateExistingProduct(productId, product);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(productId, result.Id);
            Assert.Equal("Test123", result.Name);
            Assert.Equal(12m, result.Price);
            Assert.Equal("Test123", result.Description);
            Assert.Equal(12, result.Stock);
        }

        [Fact]
        public async void UpdateExistingProduct_ShouldReturnNull_WhenProductToUpdateDoesNotExist()
        {
            // Arrange
            int productId = 1;

            await _context.Database.EnsureDeletedAsync();

            Product product = new Product
            {
                Id = 1,
                Name = "Test123",
                Price = 12m,
                Description = "Test123",
                Stock = 12
            };

            // Act
            var result = await _productRepository.UpdateExistingProduct(productId, product);

            // Assert
            Assert.Null(result);
        }

        private Product Product()
        {
            return new Product
            {
                Name = "TestName1",
                Price = 19.95m,
                Description = "TestDescription",
                Stock = 1
            };
        }

        private Product Product(int productId)
        {
            return new Product
            {
                Id = productId,
                Name = "TestName1",
                Price = 19.95m,
                Description = "TestDescription",
                Stock = 1
            };
        }

        private List<Product> ProductList(int productId1, int productId2)
        {
            return new List<Product>()
            {
                new Product
                {
                    Id = productId1,
                    Name = "TestName1",
                    Price = 19.95m,
                    Description = "TestDescription",
                    Stock = 1
                },
                new Product
                {
                    Id = productId2,
                    Name = "TestName2",
                    Price = 29.95m,
                    Description = "TestDescription2",
                    Stock = 2
                }
            };
        }
    }
}
