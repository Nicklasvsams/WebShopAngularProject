using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using WebShopLibrary.BusinessAccessLayer.DTOs.PurchaseDTO;
using WebShopLibrary.BusinessAccessLayer.Services.TransactionServices;
using WebShopLibrary.WebApi.Controllers.TransactionControllers;
using Xunit;

namespace WebShopLibrary.Tests.Controllers
{
    public class PurchaseControllerTests
    {
        private readonly PurchaseController _purchaseController;
        private readonly Mock<IPurchaseService> _mockPurchaseService = new Mock<IPurchaseService>();

        public PurchaseControllerTests()
        {
            _purchaseController = new PurchaseController(_mockPurchaseService.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_WhenNoPurchasesExist()
        {
            // Arrange
            List<PurchaseResponse> purchases = new List<PurchaseResponse>();

            _mockPurchaseService
                .Setup(x => x.GetAllPurchases())
                .ReturnsAsync(purchases);

            // Act
            var result = await _purchaseController.GetAll();
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenPurchasesExist()
        {
            // Arrange
            List<PurchaseResponse> purchases = new List<PurchaseResponse>();

            purchases.AddRange(PurchaseResponseList());

            _mockPurchaseService
                .Setup(x => x.GetAllPurchases())
                .ReturnsAsync(purchases);

            // Act
            var result = await _purchaseController.GetAll();
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenServiceReturnsNull()
        {
            // Arrange
            _mockPurchaseService
                .Setup(x => x.GetAllPurchases())
                .ReturnsAsync(() => null);

            // Act
            var result = await _purchaseController.GetAll();
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _mockPurchaseService
                .Setup(x => x.GetAllPurchases())
                .ReturnsAsync(() => throw new Exception("test"));

            // Act
            var result = await _purchaseController.GetAll();
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode200_WhenPurchaseExists()
        {
            // Arrange
            int purchaseId = 1;

            _mockPurchaseService
                .Setup(x => x.GetPurchaseById(It.IsAny<int>()))
                .ReturnsAsync(PurchaseResponse());

            // Act
            var result = await _purchaseController.GetById(purchaseId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int purchaseId = 1;

            _mockPurchaseService
                .Setup(x => x.GetPurchaseById(It.IsAny<int>()))
                .ReturnsAsync(() => throw new Exception("Test"));

            // Act
            var result = await _purchaseController.GetById(purchaseId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenPurchaseDoesNotExist()
        {
            // Arrange
            int purchaseId = 1;

            _mockPurchaseService
                .Setup(x => x.GetPurchaseById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _purchaseController.GetById(purchaseId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode200_WhenPurchaseIsCreated()
        {
            // Arrange
            _mockPurchaseService
                .Setup(x => x.CreatePurchase(It.IsAny<PurchaseRequest>()))
                .ReturnsAsync(PurchaseResponse());

            // Act
            var result = await _purchaseController.Create(PurchaseRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _mockPurchaseService
                .Setup(x => x.CreatePurchase(It.IsAny<PurchaseRequest>()))
                .ReturnsAsync(() => throw new Exception("Test"));

            // Act
            var result = await _purchaseController.Create(PurchaseRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenUpdateIsSuccessful()
        {
            // Arrange
            int purchaseId = 1;

            _mockPurchaseService
                .Setup(x => x.UpdatePurchase(It.IsAny<int>(), It.IsAny<PurchaseRequest>()))
                .ReturnsAsync(PurchaseResponse());

            // Act
            var result = await _purchaseController.Update(purchaseId, PurchaseRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode404_WhenPurchaseToUpdateIsNotFound()
        {
            // Arrange
            int purchaseId = 1;

            _mockPurchaseService
                .Setup(x => x.UpdatePurchase(It.IsAny<int>(), It.IsAny<PurchaseRequest>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _purchaseController.Update(purchaseId, PurchaseRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int purchaseId = 1;

            _mockPurchaseService
                .Setup(x => x.UpdatePurchase(It.IsAny<int>(), It.IsAny<PurchaseRequest>()))
                .ReturnsAsync(() => throw new Exception("Test"));

            // Act
            var result = await _purchaseController.Update(purchaseId, PurchaseRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode200_WhenDeleteIsSuccessful()
        {
            // Arrange
            int purchaseId = 1;

            _mockPurchaseService
                .Setup(x => x.DeletePurchase(It.IsAny<int>()))
                .ReturnsAsync(PurchaseResponse());

            // Act
            var result = await _purchaseController.Delete(purchaseId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode404_WhenPurchaseToDeleteIsNotFound()
        {
            // Arrange
            int purchaseId = 1;

            _mockPurchaseService
                .Setup(x => x.DeletePurchase(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _purchaseController.Delete(purchaseId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int purchaseId = 1;

            _mockPurchaseService
                .Setup(x => x.DeletePurchase(It.IsAny<int>()))
                .ReturnsAsync(() => throw new Exception("Test"));

            // Act
            var result = await _purchaseController.Delete(purchaseId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        private PurchaseResponse PurchaseResponse()
        {
            return new PurchaseResponse()
            {
                Id = 1,
                PurchaseDate = DateTime.Now,
                ProductId = 1,
                UserId = 1,
                User = new PurchaseUserResponse(),
                Product = new PurchaseProductResponse()
            };
        }

        private PurchaseRequest PurchaseRequest()
        {
            return new PurchaseRequest()
            {
                PurchaseDate = DateTime.Now,
                ProductId = 1,
                UserId = 1
            };
        }

        private List<PurchaseResponse> PurchaseResponseList()
        {
            return new List<PurchaseResponse>()
            {
                new PurchaseResponse()
                {
                    Id = 1,
                    PurchaseDate = DateTime.Now,
                    ProductId = 1,
                    UserId = 1,
                    User = new PurchaseUserResponse(),
                    Product = new PurchaseProductResponse()
                },
                new PurchaseResponse()
                {
                    Id = 2,
                    PurchaseDate = DateTime.Now,
                    ProductId = 2,
                    UserId = 2,
                    User = new PurchaseUserResponse(),
                    Product = new PurchaseProductResponse()
                }
            };
        }
    }
}
