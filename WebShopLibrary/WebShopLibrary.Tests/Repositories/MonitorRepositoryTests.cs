using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebShopLibrary.DataAccessLayer.Database;
using WebShopLibrary.DataAccessLayer.Database.Entities.Products;
using WebShopLibrary.DataAccessLayer.Database.Entities.Products.Monitors;
using WebShopLibrary.DataAccessLayer.Repositories.ProductRepositories;
using Xunit;

namespace WebShopLibrary.Tests.Repositories
{
    public class MonitorRepositoryTests
    {
        private readonly WebShopDBContext _context;
        private readonly MonitorRepository _monitorRepository;
        private readonly DbContextOptions<WebShopDBContext> _options;

        public MonitorRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<WebShopDBContext>()
                .UseInMemoryDatabase(databaseName: "WebShopMonitor")
                .Options;
            _context = new WebShopDBContext(_options);
            _monitorRepository = new MonitorRepository(_context);
        }

        [Fact]
        public async void SelectAllMonitors_ShouldReturnListOfMonitors_WhenMonitorsExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            _context.Monitor.AddRange(MonitorList());

            await _context.SaveChangesAsync();

            // Act
            var result = await _monitorRepository.SelectAllMonitors();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Monitor>>(result);
        }

        [Fact]
        public async void SelectAllMonitors_ShouldReturnEmptyList_WhenNoMonitorsExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _monitorRepository.SelectAllMonitors();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async void InsertNewMonitor_ShouldAddIdAndReturnMonitor_WhenMonitorIsInserted()
        {
            // Arrange
            int monitorId = 1;

            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _monitorRepository.InsertNewMonitor(Monitor());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Monitor>(result);
            Assert.Equal(monitorId, result.Id);
            Assert.Equal("TestBrand", result.Brand);
            Assert.Equal(1991, result.ReleaseYear);
            Assert.Equal(43, result.Size);
            Assert.Equal(1, result.CategoryId);
            Assert.Equal(1, result.ProductId);
            Assert.Equal(1, result.Product.Id);
            Assert.Equal("TestName", result.Product.Name);
            Assert.Equal("TestDescription", result.Product.Description);
            Assert.Equal(100, result.Product.Price);
            Assert.Equal(10, result.Product.Stock);
            Assert.Equal(1, result.Category.Id);
            Assert.Equal("TestName", result.Category.Name);
            Assert.Equal("TestDescription", result.Category.Description);
        }

        [Fact]
        public async void InsertNewMonitor_ShouldFailToAddMonitor_WhenMonitorWithSameIdAlreadyExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            Monitor monitor = new Monitor { Id = 1, Brand = "", CategoryId = 1, ReleaseYear = 1, ProductId = 1, Size = 1, Category = new Category(), Product = new Product() };

            _context.Add(monitor);

            await _context.SaveChangesAsync();

            // Act
            async Task action() => await _monitorRepository.InsertNewMonitor(monitor);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async void SelectMonitorById_ShouldReturnMonitor_WhenMonitorExists()
        {
            // Arrange
            int monitorId = 1;

            await _context.Database.EnsureDeletedAsync();

            _context.Monitor.Add(Monitor());

            await _context.SaveChangesAsync();

            // Act
            var result = await _monitorRepository.SelectMonitorById(monitorId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Monitor>(result);
            Assert.Equal(monitorId, result.Id);
            Assert.Equal("TestBrand", result.Brand);
            Assert.Equal(1991, result.ReleaseYear);
            Assert.Equal(43, result.Size);
            Assert.Equal(1, result.CategoryId);
            Assert.Equal(1, result.ProductId);
            Assert.Equal(1, result.Product.Id);
            Assert.Equal("TestName", result.Product.Name);
            Assert.Equal("TestDescription", result.Product.Description);
            Assert.Equal(100, result.Product.Price);
            Assert.Equal(10, result.Product.Stock);
            Assert.Equal(1, result.Category.Id);
            Assert.Equal("TestName", result.Category.Name);
            Assert.Equal("TestDescription", result.Category.Description);
        }

        [Fact]
        public async void SelectMonitorById_ShouldReturnNull_WhenMonitorDoesNotExist()
        {
            // Arrange
            int monitorId = 1;

            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _monitorRepository.SelectMonitorById(monitorId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeleteMonitor_ShouldReturnMonitor_WhenMonitorIsDeleted()
        {
            // Arrange
            int monitorId = 1;
            
            await _context.Database.EnsureDeletedAsync();

            _context.Monitor.Add(Monitor());

            await _context.SaveChangesAsync();

            // Act
            var result = await _monitorRepository.DeleteMonitorById(monitorId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Monitor>(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async void DeleteMonitor_ShouldReturnNull_WhenMonitorToDeleteDoesNotExist()
        {
            // Arrange
            int monitorId = 1;

            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _monitorRepository.DeleteMonitorById(monitorId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void UpdateExistingMonitor_ShouldReturnMonitor_WhenUpdateIsSuccessful()
        {
            // Arrange
            int monitorId = 1;

            await _context.Database.EnsureDeletedAsync();

            _context.Add(Monitor());

            await _context.SaveChangesAsync();

            Monitor monitorUpdate = new Monitor
            {
                Brand = "TestBrand2",
                ReleaseYear = 1992,
                Size = 44,
                CategoryId = 2,
                ProductId = 2,
                Category = new Category
                {
                    Id = 2,
                    Name = "TestName2",
                    Description = "TestDescription2"
                },
                Product = new Product
                {
                    Id = 2,
                    Name = "TestName2",
                    Description = "TestDescription2",
                    Price = 200,
                    Stock = 20
                }
            };

            // Act
            var result = await _monitorRepository.UpdateExistingMonitor(monitorId, monitorUpdate);

            // Assert

            Assert.NotNull(result);
            Assert.IsType<Monitor>(result);
            Assert.Equal(monitorId, result.Id);
            Assert.Equal("TestBrand2", result.Brand);
            Assert.Equal(1992, result.ReleaseYear);
            Assert.Equal(44, result.Size);
            Assert.Equal(2, result.CategoryId);
            Assert.Equal(2, result.ProductId);
        }

        [Fact]
        public async void UpdateExistingMonitor_ShouldReturnNull_WhenMonitorToUpdateDoesNotExist()
        {
            // Arrange
            int monitorId = 1;

            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _monitorRepository.UpdateExistingMonitor(monitorId, Monitor());

            // Assert
            Assert.Null(result);
        }

        private List<Monitor> MonitorList()
        {
            List<Monitor> monitors = new List<Monitor>();

            monitors.Add(new Monitor
            {
                Id = 1,
                Brand = "TestBrand",
                ReleaseYear = 1991,
                Size = 43,
                CategoryId = 1,
                ProductId = 1,
                Category = new Category
                {
                    Id = 1,
                    Name = "TestName",
                    Description = "TestDescription"
                },
                Product = new Product
                {
                    Id = 1,
                    Name = "TestName2",
                    Description = "TestDescription2",
                    Price = 100,
                    Stock = 10
                }
            });

            monitors.Add(
                new Monitor
                {
                    Id = 2,
                    Brand = "TestBrand2",
                    ReleaseYear = 1992,
                    Size = 47,
                    CategoryId = 2,
                    ProductId = 2,
                    Category = new Category
                    {
                        Id = 2,
                        Name = "TestName2",
                        Description = "TestDescription2"
                    },
                    Product = new Product
                    {
                        Id = 2,
                        Name = "TestName2",
                        Description = "TestDescription2",
                        Price = 200,
                        Stock = 20
                    }
                });

            return monitors;
        }

        private Monitor Monitor()
        {
            return new Monitor
            {
                Id = 1,
                Brand = "TestBrand",
                ReleaseYear = 1991,
                Size = 43,
                CategoryId = 1,
                ProductId = 1,
                Category = new Category
                {
                    Id = 1,
                    Name = "TestName",
                    Description = "TestDescription"
                },
                Product = new Product
                {
                    Id = 1,
                    Name = "TestName",
                    Description = "TestDescription",
                    Price = 100,
                    Stock = 10
                }
            };
        }
    }
}
