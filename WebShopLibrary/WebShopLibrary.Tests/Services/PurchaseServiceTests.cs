using Moq;
using System;
using System.Collections.Generic;
using WebShopLibrary.BusinessAccessLayer.DTOs.PurchaseDTO;
using WebShopLibrary.BusinessAccessLayer.Services.TransactionServices;
using WebShopLibrary.DataAccessLayer.Database.Entities.Products;
using WebShopLibrary.DataAccessLayer.Database.Entities.Transactions;
using WebShopLibrary.DataAccessLayer.Database.Entities.Users;
using WebShopLibrary.DataAccessLayer.Repositories.TransactionRepositories;
using Xunit;

namespace WebShopLibrary.Tests.Services
{
    public class PurchaseServiceTests
    {
        private readonly IPurchaseService _purchaseService;
        private readonly Mock<IPurchaseRepository> _mockPurchaseRepository = new Mock<IPurchaseRepository>();

        public PurchaseServiceTests()
        {
            _purchaseService = new PurchaseService(_mockPurchaseRepository.Object);
        }

        [Fact]
        public async void GetAllPurchases_ShouldReturnListOfPurchaseResponses_WhenPurchasesExist()
        {
            // Arrange
            _mockPurchaseRepository
                .Setup(x => x.SelectAllPurchases())
                .ReturnsAsync(PurchaseList());

            // Act
            var result = await _purchaseService.GetAllPurchases();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<PurchaseResponse>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetAllPurchases_ShouldReturnEmptyListOfPurchaseResponses_WhenNoPurchasesExist()
        {
            // Arrange
            List<Purchase> purchases = new List<Purchase>();

            _mockPurchaseRepository
                .Setup(x => x.SelectAllPurchases())
                .ReturnsAsync(purchases);

            // Act
            var result = await _purchaseService.GetAllPurchases();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<PurchaseResponse>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void GetPurchaseById_ShouldReturnSinglePurchaseResponse_WhenPurchaseExists()
        {
            // Arrange
            int purchaseId = 1;

            _mockPurchaseRepository
                .Setup(x => x.SelectPurchaseById(It.IsAny<int>()))
                .ReturnsAsync(Purchase());

            // Act
            var result = await _purchaseService.GetPurchaseById(purchaseId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PurchaseResponse>(result);
            Assert.Equal(purchaseId, result.Id);
            Assert.Equal(1, result.ProductId);
            Assert.Equal(1, result.UserId);
            Assert.IsType<PurchaseUserResponse>(result.User);
            Assert.IsType<PurchaseProductResponse>(result.Product);
        }

        [Fact]
        public async void GetPurchaseById_ShouldReturnNull_WhenPurchaseDoesNotExist()
        {
            // Arrange
            int purchaseId = 1;

            _mockPurchaseRepository
                .Setup(x => x.SelectPurchaseById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _purchaseService.GetPurchaseById(purchaseId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void UpdatePurchase_ShouldReturnSinglePurchaseResponse_WhenPurchaseExists()
        {
            // Arrange
            int purchaseId = 1;

            _mockPurchaseRepository
                .Setup(x => x.UpdateExistingPurchaseById(It.IsAny<int>(), It.IsAny<Purchase>()))
                .ReturnsAsync(Purchase());

            // Act
            var result = await _purchaseService.UpdatePurchase(purchaseId, PurchaseRequest());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PurchaseResponse>(result);
            Assert.Equal(purchaseId, result.Id);
            Assert.Equal(1, result.ProductId);
            Assert.Equal(1, result.UserId);
            Assert.IsType<PurchaseUserResponse>(result.User);
            Assert.IsType<PurchaseProductResponse>(result.Product);
        }

        [Fact]
        public async void UpdatePurchase_ShouldReturnNull_WhenPurchaseDoesNotExist()
        {
            // Arrange
            int purchaseId = 1;

            _mockPurchaseRepository
                .Setup(x => x.UpdateExistingPurchaseById(It.IsAny<int>(), It.IsAny<Purchase>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _purchaseService.UpdatePurchase(purchaseId, PurchaseRequest());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void CreatePurchase_ShouldReturnPurchaseResponse_WhenPurchaseIsCreated()
        {
            // Arrange
            int purchaseId = 1;

            _mockPurchaseRepository
                .Setup(x => x.InsertNewPurchase(It.IsAny<Purchase>()))
                .ReturnsAsync(Purchase());

            // Act
            var result = await _purchaseService.CreatePurchase(PurchaseRequest());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PurchaseResponse>(result);
            Assert.Equal(purchaseId, result.Id);
            Assert.Equal(1, result.ProductId);
            Assert.Equal(1, result.UserId);
            Assert.IsType<PurchaseUserResponse>(result.User);
            Assert.IsType<PurchaseProductResponse>(result.Product);
        }

        [Fact]
        public async void CreatePurchase_ShouldReturnNull_WhenPurchaseIsNotCreated()
        {
            // Arrange
            _mockPurchaseRepository
                .Setup(x => x.InsertNewPurchase(It.IsAny<Purchase>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _purchaseService.CreatePurchase(PurchaseRequest());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeletePurchase_ShouldReturnPurchaseResponse_WhenPurchaseIsDeleted()
        {
            // Arrange
            int purchaseId = 1;

            _mockPurchaseRepository
                .Setup(x => x.DeletePurchaseById(It.IsAny<int>()))
                .ReturnsAsync(Purchase());

            // Act
            var result = await _purchaseService.DeletePurchase(purchaseId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PurchaseResponse>(result);
            Assert.Equal(purchaseId, result.Id);
            Assert.Equal(1, result.ProductId);
            Assert.Equal(1, result.UserId);
            Assert.IsType<PurchaseUserResponse>(result.User);
            Assert.IsType<PurchaseProductResponse>(result.Product);
        }

        [Fact]
        public async void DeletePurchase_ShouldReturnNull_WhenPurchaseDoesNotExist()
        {
            // Arrange
            int purchaseId = 1;

            _mockPurchaseRepository
                .Setup(x => x.DeletePurchaseById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _purchaseService.DeletePurchase(purchaseId);

            // Assert
            Assert.Null(result);
        }

        private Purchase Purchase()
        {
            return new Purchase
            {
                Id = 1,
                PurchaseDate = DateTime.Now,
                ProductId = 1,
                UserId = 1,
                User = new User(),
                Product = new Product()
            };
        }

        private List<Purchase> PurchaseList()
        {
            return new List<Purchase>()
            {
                new Purchase
                {
                    Id = 1,
                    PurchaseDate = DateTime.Now,
                    ProductId = 1,
                    UserId = 1,
                    User = new User(),
                    Product = new Product()
                },
                new Purchase
                {
                    Id = 2,
                    PurchaseDate = DateTime.Now,
                    ProductId = 2,
                    UserId = 2,
                    User = new User(),
                    Product = new Product()
                }
            };
        }

        private PurchaseRequest PurchaseRequest()
        {
            return new PurchaseRequest
            {
                PurchaseDate = DateTime.Now,
                ProductId = 1,
                UserId = 1
            };
        }
    }
}
