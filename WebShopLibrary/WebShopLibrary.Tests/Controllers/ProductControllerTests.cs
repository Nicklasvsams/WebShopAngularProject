using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using WebShopLibrary.BusinessAccessLayer.DTOs.ProductDTO;
using WebShopLibrary.BusinessAccessLayer.Services.ProductServices;
using WebShopLibrary.WebApi.Controllers.ProductControllers;
using Xunit;

namespace WebShopLibrary.Tests.Controllers
{
    public class ProductControllerTests
    {
        private readonly ProductController _productController;
        private readonly Mock<IProductService> _mockProductService = new Mock<IProductService>();

        public ProductControllerTests()
        {
            _productController = new ProductController(_mockProductService.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_WhenNoProductsExist()
        {
            // Arrange
            List<ProductResponse> products = new List<ProductResponse>();

            _mockProductService
                .Setup(x => x.GetAllProducts())
                .ReturnsAsync(products);

            // Act
            var result = await _productController.GetAll();
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenProductsExist()
        {
            // Arrange
            List<ProductResponse> products = new List<ProductResponse>();

            products.AddRange(ProductResponseList());

            _mockProductService
                .Setup(x => x.GetAllProducts())
                .ReturnsAsync(products);

            // Act
            var result = await _productController.GetAll();
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenServiceReturnsNull()
        {
            // Arrange
            List<ProductResponse> products = new List<ProductResponse>();

            _mockProductService
                .Setup(x => x.GetAllProducts())
                .ReturnsAsync(() => null);

            // Act
            var result = await _productController.GetAll();
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _mockProductService
                .Setup(x => x.GetAllProducts())
                .ReturnsAsync(() => throw new Exception("test"));

            // Act
            var result = await _productController.GetAll();
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode200_WhenProductExists()
        {
            // Arrange
            int productId = 1;

            _mockProductService
                .Setup(x => x.GetProductById(It.IsAny<int>()))
                .ReturnsAsync(ProductResponse);

            // Act
            var result = await _productController.GetById(productId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int productId = 1;

            _mockProductService
                .Setup(x => x.GetProductById(It.IsAny<int>()))
                .ReturnsAsync(() => throw new Exception("Test"));

            // Act
            var result = await _productController.GetById(productId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenProductDoesNotExist()
        {
            // Arrange
            int productId = 1;

            _mockProductService
                .Setup(x => x.GetProductById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _productController.GetById(productId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode200_WhenProductIsCreated()
        {
            // Arrange
            _mockProductService
                .Setup(x => x.CreateProduct(It.IsAny<ProductRequest>()))
                .ReturnsAsync(ProductResponse());

            // Act
            var result = await _productController.Create(ProductRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _mockProductService
                .Setup(x => x.CreateProduct(It.IsAny<ProductRequest>()))
                .ReturnsAsync(() => throw new Exception("Test"));

            // Act
            var result = await _productController.Create(ProductRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenUpdateIsSuccessful()
        {
            // Arrange
            int productId = 1;

            _mockProductService
                .Setup(x => x.UpdateProduct(It.IsAny<int>(), It.IsAny<ProductRequest>()))
                .ReturnsAsync(ProductResponse());

            // Act
            var result = await _productController.Update(productId, ProductRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode404_WhenProductToUpdateIsNotFound()
        {
            // Arrange
            int productId = 1;

            _mockProductService
                .Setup(x => x.UpdateProduct(It.IsAny<int>(), It.IsAny<ProductRequest>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _productController.Update(productId, ProductRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int productId = 1;

            _mockProductService
                .Setup(x => x.UpdateProduct(productId, It.IsAny<ProductRequest>()))
                .ReturnsAsync(() => throw new Exception("Test"));

            // Act
            var result = await _productController.Update(productId, ProductRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode200_WhenDeleteIsSuccessful()
        {
            // Arrange
            int productId = 1;

            _mockProductService
                .Setup(x => x.DeleteProduct(productId))
                .ReturnsAsync(ProductResponse());

            // Act
            var result = await _productController.Delete(productId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode404_WhenProductToDeleteIsNotFound()
        {
            // Arrange
            int productId = 1;

            _mockProductService
                .Setup(x => x.DeleteProduct(productId))
                .ReturnsAsync(() => null);

            // Act
            var result = await _productController.Delete(productId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int productId = 1;

            _mockProductService
                .Setup(x => x.DeleteProduct(productId))
                .ReturnsAsync(() => throw new Exception("Test"));

            // Act
            var result = await _productController.Delete(productId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        private ProductResponse ProductResponse()
        {
            return new ProductResponse()
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                Price = 10m,
                Stock = 10
            };
        }

        private ProductRequest ProductRequest()
        {
            return new ProductRequest()
            {
                Name = "Test",
                Description = "Test",
                Price = 10m,
                Stock = 10
            };
        }

        private List<ProductResponse> ProductResponseList()
        {
            return new List<ProductResponse>()
            {
                new ProductResponse()
                {
                    Id = 1,
                    Name = "Test",
                    Description = "Test",
                    Price = 10m,
                    Stock = 10
                },
                new ProductResponse()
                {
                    Id = 2,
                    Name = "Test2",
                    Description = "Test2",
                    Price = 20m,
                    Stock = 20
                }
            };
        }
    }
}
