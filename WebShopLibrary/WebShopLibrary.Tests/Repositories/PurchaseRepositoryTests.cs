using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebShopLibrary.BusinessAccessLayer.DTOs.PurchaseDTO;
using WebShopLibrary.DataAccessLayer.Database;
using WebShopLibrary.DataAccessLayer.Database.Entities.Products;
using WebShopLibrary.DataAccessLayer.Database.Entities.Transactions;
using WebShopLibrary.DataAccessLayer.Database.Entities.Users;
using WebShopLibrary.DataAccessLayer.Repositories.TransactionRepositories;
using Xunit;

namespace WebShopLibrary.Tests.Repositories
{
    public class PurchaseRepositoryTests
    {
        private readonly DbContextOptions<WebShopDBContext> _options;
        private readonly WebShopDBContext _context;
        private readonly PurchaseRepository _purchaseRepository;

        public PurchaseRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<WebShopDBContext>()
                .UseInMemoryDatabase(databaseName: "WebShopPurchase")
                .Options;
            _context = new WebShopDBContext(_options);
            _purchaseRepository = new PurchaseRepository(_context);
        }

        [Fact]
        public async void SelectAllPurchases_ShouldReturnListOfPurchases_WhenPurchasesExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            _context.AddRange(PurchaseList());

            await _context.SaveChangesAsync();

            // Act
            var result = await _purchaseRepository.SelectAllPurchases();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Purchase>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void SelectAllPurchases_ShouldReturnEmptyListOfPurchases_WhenNoPurchasesExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _purchaseRepository.SelectAllPurchases();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Purchase>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void DeletePurchaseById_ShouldReturnDeletedPurchase_WhenPurchaseExists()
        {
            // Arrange
            int purchaseId = 1;

            await _context.Database.EnsureDeletedAsync();

            _context.Add(Purchase());

            await _context.SaveChangesAsync();

            // Act
            var result = await _purchaseRepository.DeletePurchaseById(purchaseId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Purchase>(result);
            Assert.Equal(purchaseId, result.Id);
        }

        [Fact]
        public async void DeletePurchaseById_ShouldReturnNull_WhenPurchaseDoesNotExist()
        {
            // Arrange
            int purchaseId = 1;

            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _purchaseRepository.DeletePurchaseById(purchaseId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void SelectPurchaseById_ShouldReturnPurchase_WhenPurchaseExists()
        {
            // Arrange
            int purchaseId = 1;

            await _context.Database.EnsureDeletedAsync();

            _context.Purchase.Add(Purchase());

            await _context.SaveChangesAsync();

            // Act
            var result = await _purchaseRepository.SelectPurchaseById(purchaseId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Purchase>(result);
            Assert.Equal(purchaseId, result.Id);
            Assert.Equal(1, result.UserId);
            Assert.Equal(1, result.ProductId);
        }

        [Fact]
        public async void SelectPurchaseById_ShouldReturnNull_WhenPurchaseDoesNotExist()
        {
            // Arrange
            int purchaseId = 1;

            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _purchaseRepository.SelectPurchaseById(purchaseId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void InsertNewPurchase_ShouldAddIdAndReturnPurchase_WhenPurchaseIsSuccessfullyCreated()
        {
            // Arrange
            int purchaseId = 1;

            await _context.Database.EnsureDeletedAsync();

            Purchase purchase = new Purchase()
            {
                PurchaseDate = DateTime.Now,
                ProductId = 1,
                UserId = 1
            };

            // Act
            var result = await _purchaseRepository.InsertNewPurchase(purchase);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Purchase>(result);
            Assert.Equal(purchaseId, result.Id);
            Assert.Equal(1, result.ProductId);
            Assert.Equal(1, result.UserId);
        }

        [Fact]
        public async void InsertNewPurchase_ShouldFailToAddPurchase_WhenPurchaseWithSameIdAlreadyExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            Purchase purchase = new Purchase
            {
                Id = 1,
                PurchaseDate = DateTime.Now,
                ProductId = 1,
                UserId = 1
            };

            _context.Add(purchase);

            await _context.SaveChangesAsync();

            // Act
            async Task action() => await _purchaseRepository.InsertNewPurchase(purchase);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async void UpdateExistingPurchase_ShouldReturnPurchase_WhenPurchaseExists()
        {
            // Arrange
            int purchaseId = 1;

            await _context.Database.EnsureDeletedAsync();

            _context.Purchase.Add(Purchase());

            await _context.SaveChangesAsync();

            Purchase purchase = new Purchase()
            {
                PurchaseDate = DateTime.Now,
                ProductId = 2,
                UserId = 2
            };

            // Act
            var result = await _purchaseRepository.UpdateExistingPurchaseById(purchaseId, purchase);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Purchase>(result);
            Assert.Equal(purchaseId, result.Id);
            Assert.Equal(2, result.ProductId);
            Assert.Equal(2, result.UserId);
        }

        [Fact]
        public async void UpdateExistingPurchase_ShouldReturnNull_WhenPurchaseToUpdateDoesNotExist()
        {
            // Arrange
            int purchaseId = 1;

            await _context.Database.EnsureDeletedAsync();

            Purchase purchase = new Purchase()
            {
                PurchaseDate = DateTime.Now,
                ProductId = 1,
                UserId = 1
            };

            // Act
            var result = await _purchaseRepository.UpdateExistingPurchaseById(purchaseId, purchase);

            // Assert
            Assert.Null(result);
        }

        private Purchase Purchase()
        {
            return new Purchase
            {
                Id = 1,
                PurchaseDate = DateTime.Now,
                UserId = 1,
                ProductId = 1,
                Product = new Product(),
                User = new User()
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
                    UserId = 1,
                    ProductId = 1,
                    Product = new Product(),
                    User = new User()
                },
                new Purchase
                {
                    Id = 2,
                    PurchaseDate = DateTime.Now,
                    UserId = 2,
                    ProductId = 2,
                    Product = new Product(),
                    User = new User()
                }
            };
        }

        private PurchaseRequest PurchaseRequest()
        {
            return new PurchaseRequest
            {
                PurchaseDate = DateTime.Now,
                UserId = 1,
                ProductId = 1
            };
        }
    }
}
