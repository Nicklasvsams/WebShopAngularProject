using Moq;
using System.Collections.Generic;
using WebShopLibrary.BusinessAccessLayer.DTOs.ProductDTO;
using WebShopLibrary.BusinessAccessLayer.Services.ProductServices;
using WebShopLibrary.DataAccessLayer.Database.Entities.Products;
using WebShopLibrary.DataAccessLayer.Repositories.ProductRepositories;
using Xunit;

namespace WebShopLibrary.Tests.Services
{
    public class ProductServiceTests
    {
        private readonly ProductService _productervice;
        private readonly Mock<IProductRepository> _mockProductRepository = new Mock<IProductRepository>();

        public ProductServiceTests()
        {
            _productervice = new ProductService(_mockProductRepository.Object);
        }

        [Fact]
        public async void GetAllProducts_ShouldReturnListOfProductResponses_WhenProductsExist()
        {
            // Arrange
            int productId1 = 1;
            int productId2 = 2;

            _mockProductRepository
                .Setup(x => x.SelectAllProducts())
                .ReturnsAsync(ProductList(productId1, productId2));

            // Act
            var result = await _productervice.GetAllProducts();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ProductResponse>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetAllProducts_ShouldReturnEmptyListOfProductResponses_WhenNoProductsExist()
        {
            // Arrange
            List<Product> products = new List<Product>();

            _mockProductRepository
                .Setup(x => x.SelectAllProducts())
                .ReturnsAsync(products);

            // Act
            var result = await _productervice.GetAllProducts();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ProductResponse>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void GetProductById_ShouldReturnSingleProductResponse_WhenProductExists()
        {
            // Arrange
            int productId = 1;

            _mockProductRepository
                .Setup(x => x.SelectProductById(It.IsAny<int>()))
                .ReturnsAsync(Product(productId));

            // Act
            var result = await _productervice.GetProductById(productId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProductResponse>(result);
            Assert.Equal(productId, result.Id);
            Assert.Equal("Test", result.Name);
            Assert.Equal(1m, result.Price);
            Assert.Equal("Test", result.Description);
            Assert.Equal(1, result.Stock);
        }

        [Fact]
        public async void GetProductById_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            int productId = 1;

            _mockProductRepository
                .Setup(x => x.SelectProductById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _productervice.GetProductById(productId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void UpdateProduct_ShouldReturnSingleProductResponse_WhenProductExists()
        {
            // Arrange
            int productId = 1;

            _mockProductRepository
                .Setup(x => x.UpdateExistingProduct(It.IsAny<int>(), It.IsAny<Product>()))
                .ReturnsAsync(Product(productId));

            // Act
            var result = await _productervice.UpdateProduct(productId, ProductRequest());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProductResponse>(result);
            Assert.Equal(productId, result.Id);
            Assert.Equal("Test", result.Name);
            Assert.Equal(1m, result.Price);
            Assert.Equal("Test", result.Description);
            Assert.Equal(1, result.Stock);
        }

        [Fact]
        public async void UpdateProduct_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            int productId = 1;

            _mockProductRepository
                .Setup(x => x.UpdateExistingProduct(It.IsAny<int>(), It.IsAny<Product>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _productervice.UpdateProduct(productId, ProductRequest());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void CreateProduct_ShouldReturnProductResponse_WhenProductIsCreated()
        {
            // Arrange
            int productId = 1;

            _mockProductRepository
                .Setup(x => x.InsertNewProduct(It.IsAny<Product>()))
                .ReturnsAsync(Product(productId));

            // Act
            var result = await _productervice.CreateProduct(ProductRequest());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProductResponse>(result);
            Assert.Equal(productId, result.Id);
            Assert.Equal("Test", result.Name);
            Assert.Equal(1m, result.Price);
            Assert.Equal("Test", result.Description);
            Assert.Equal(1, result.Stock);
        }

        [Fact]
        public async void CreateProduct_ShouldReturnNull_WhenProductIsNotCreated()
        {
            // Arrange
            _mockProductRepository
                .Setup(x => x.InsertNewProduct(It.IsAny<Product>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _productervice.CreateProduct(ProductRequest());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeleteProduct_ShouldReturnProductResponse_WhenProductIsDeleted()
        {
            // Arrange
            int productId = 1;

            _mockProductRepository
                .Setup(x => x.DeleteProductById(It.IsAny<int>()))
                .ReturnsAsync(Product(productId));

            // Act
            var result = await _productervice.DeleteProduct(productId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProductResponse>(result);
            Assert.Equal(productId, result.Id);
            Assert.Equal("Test", result.Name);
            Assert.Equal(1m, result.Price);
            Assert.Equal("Test", result.Description);
            Assert.Equal(1, result.Stock);
        }

        [Fact]
        public async void DeleteProduct_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            int productId = 1;

            _mockProductRepository
                .Setup(x => x.DeleteProductById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _productervice.DeleteProduct(productId);

            // Assert
            Assert.Null(result);
        }

        private Product Product(int productId)
        {
            return new Product
            {
                Id = productId,
                Name = "Test",
                Description = "Test",
                Price = 1m,
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
                    Name = "Test",
                    Description = "Test",
                    Price = 1m,
                    Stock = 1
                },
                new Product
                {
                    Id = productId2,
                    Name = "Test2",
                    Description = "Test2",
                    Price = 2m,
                    Stock = 2
                }
            };
        }

        private ProductRequest ProductRequest()
        {
            return new ProductRequest
            {
                Name = "Test",
                Description = "Test",
                Price = 1m,
                Stock = 1
            };
        }
    }
}
