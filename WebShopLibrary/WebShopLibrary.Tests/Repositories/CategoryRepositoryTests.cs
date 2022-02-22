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
    public class CategoryRepositoryTests
    {
        private readonly DbContextOptions<WebShopDBContext> _options;
        private readonly WebShopDBContext _context;
        private readonly CategoryRepository _categoryRepository;

        public CategoryRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<WebShopDBContext>()
                .UseInMemoryDatabase(databaseName: "WebShopCategory")
                .Options;
            _context = new WebShopDBContext(_options);
            _categoryRepository = new CategoryRepository(_context);
        }

        [Fact]
        public async void SelectAllCategories_ShouldReturnListOfCategories_WhenCategoriesExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            await _context.AddRangeAsync(CategoryList());

            await _context.SaveChangesAsync();

            // Act
            var result = await _categoryRepository.SelectAllCategories();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Category>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void SelectAllCategories_ShouldReturnEmptyList_WhenCategoriesIsEmpty()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _categoryRepository.SelectAllCategories();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Category>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void SelectCategoryById_ShouldReturnSingleCategory_WhenCategoryExists()
        {
            // Arrange
            int categoryId = 1;

            await _context.Database.EnsureDeletedAsync();

            await _context.Category.AddAsync(Category());

            await _context.SaveChangesAsync();

            // Act
            var result = await _categoryRepository.SelectCategoryById(categoryId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(categoryId, result.Id);
            Assert.Equal("Test", result.Name);
            Assert.Equal("Test", result.Description);
        }

        [Fact]
        public async void SelectCategoryById_ShouldReturnNull_WhenCategoryDoesNotExist()
        {
            // Arrange
            int categoryId = 1;
            
            await _context.Database.EnsureDeletedAsync();
            // Act
            var result = await _categoryRepository.SelectCategoryById(categoryId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void InsertCategory_ShouldReturnCategoryAndAddId_WhenCategoryIsCreated()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            Category category = new Category() 
            { 
                Name = "Test", 
                Description = "Test" 
            };

            // Act
            var result = await _categoryRepository.InsertNewCategory(category);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test", result.Name);
            Assert.Equal("Test", result.Description);
        }

        [Fact]
        public async void InsertCategory_ShouldFailToAddNewCategory_WhenCategoryWithSameIdAlreadyExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            Category category = new Category()
            {
                Id = 1,
                Name = "Test",
                Description = "Test"
            };

            await _context.Category.AddAsync(category);

            await _context.SaveChangesAsync();

            // Act
            async Task action() => await _categoryRepository.InsertNewCategory(category);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async void UpdateCategory_ShouldReturnCategory_WhenUpdateIsSuccessful()
        {
            // Arrange
            int categoryId = 1;

            await _context.Database.EnsureDeletedAsync();

            await _context.Category.AddAsync(Category());

            await _context.SaveChangesAsync();

            Category category = new Category()
            {
                Name = "Test2",
                Description = "Test2"
            };

            // Act
            var result = await _categoryRepository.UpdateExistingCategory(categoryId, category);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(categoryId, result.Id);
            Assert.Equal("Test2", result.Name);
            Assert.Equal("Test2", result.Description);
        }

        [Fact]
        public async void UpdateCategory_ShouldReturnNull_WhenCategoryToUpdateDoesNotExist()
        {
            // Arrange
            int categoryId = 1;

            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _categoryRepository.UpdateExistingCategory(categoryId, Category());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeleteCategory_ShouldReturnCategory_WhenCategoryIsDeleted()
        {
            // Arrange
            int categoryId = 1;

            await _context.Database.EnsureDeletedAsync();

            await _context.Category.AddAsync(Category());

            await _context.SaveChangesAsync();

            // Act
            var result = await _categoryRepository.DeleteCategoryById(categoryId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test", result.Name);
            Assert.Equal("Test", result.Description);
        }

        [Fact]
        public async void DeleteCategory_ShouldReturnNull_WhenCategoryToDeleteDoesNotExist()
        {
            // Arrange
            int categoryId = 1;

            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _categoryRepository.DeleteCategoryById(categoryId);

            // Assert
            Assert.Null(result);
        }

        private List<Category> CategoryList()
        {
            return new List<Category>()
            {
                new Category()
                {
                    Id = 1,
                    Name = "Test",
                    Description = "Test"
                },
                new Category()
                {
                    Id = 2,
                    Name = "Test2",
                    Description = "Test2"
                }
            };
        }

        private Category Category()
        {
            return new Category()
            {
                Id = 1,
                Name = "Test",
                Description = "Test"
            };
        }
    }
}
