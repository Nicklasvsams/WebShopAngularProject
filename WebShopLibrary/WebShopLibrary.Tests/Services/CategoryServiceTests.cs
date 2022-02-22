using Moq;
using System.Collections.Generic;
using WebShopLibrary.BusinessAccessLayer.DTOs.CategoryDTO;
using WebShopLibrary.BusinessAccessLayer.Services.ProductServices;
using WebShopLibrary.DataAccessLayer.Database.Entities.Products;
using WebShopLibrary.DataAccessLayer.Repositories.ProductRepositories;
using Xunit;

namespace WebShopLibrary.Tests.Services
{
    public class CategoryServiceTests
    {
        private readonly CategoryService _categoryService;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository = new Mock<ICategoryRepository>();

        public CategoryServiceTests()
        {
            _categoryService = new CategoryService(_mockCategoryRepository.Object);
        }

        [Fact]
        public async void GetAllCategories_ShouldReturnListOfCategoryResponses_WhenCategoriesExist()
        {
            // Arrange
            int categoryId1 = 1;
            int categoryId2 = 2;

            _mockCategoryRepository
                .Setup(x => x.SelectAllCategories())
                .ReturnsAsync(CategoryList(categoryId1, categoryId2));

            // Act
            var result = await _categoryService.GetAllCategories();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<CategoryResponse>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetAllCategories_ShouldReturnEmptyListOfCategoryResponses_WhenNoCategoriesExist()
        {
            // Arrange
            List<Category> categories = new List<Category>();

            _mockCategoryRepository
                .Setup(x => x.SelectAllCategories())
                .ReturnsAsync(categories);

            // Act
            var result = await _categoryService.GetAllCategories();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<CategoryResponse>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void GetCategoryById_ShouldReturnSingleCategoryResponse_WhenCategoryExists()
        {
            // Arrange
            int categoryId = 1;

            _mockCategoryRepository
                .Setup(x => x.SelectCategoryById(It.IsAny<int>()))
                .ReturnsAsync(Category(categoryId));

            // Act
            var result = await _categoryService.GetCategoryById(categoryId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CategoryResponse>(result);
            Assert.Equal(categoryId, result.Id);
            Assert.Equal("CategoryTestName", result.Name);
            Assert.Equal("CategoryTestDescription", result.Description);
        }

        [Fact]
        public async void GetCategoryById_ShouldReturnNull_WhenCategoryDoesNotExist()
        {
            // Arrange
            int categoryId = 1;

            _mockCategoryRepository
                .Setup(x => x.SelectCategoryById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _categoryService.GetCategoryById(categoryId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void UpdateCategory_ShouldReturnSingleCategoryResponse_WhenCategoryExists()
        {
            // Arrange
            int categoryId = 1;

            _mockCategoryRepository
                .Setup(x => x.UpdateExistingCategory(It.IsAny<int>(), It.IsAny<Category>()))
                .ReturnsAsync(Category(categoryId));

            // Act
            var result = await _categoryService.UpdateCategory(categoryId, CategoryRequest());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CategoryResponse>(result);
            Assert.Equal(categoryId, result.Id);
            Assert.Equal("CategoryTestName", result.Name);
            Assert.Equal("CategoryTestDescription", result.Description);
        }

        [Fact]
        public async void UpdateCategory_ShouldReturnNull_WhenCategoryDoesNotExist()
        {
            // Arrange
            int categoryId = 1;

            _mockCategoryRepository
                .Setup(x => x.UpdateExistingCategory(It.IsAny<int>(), It.IsAny<Category>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _categoryService.UpdateCategory(categoryId, CategoryRequest());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void CreateCategory_ShouldReturnCategoryResponse_WhenCategoryIsCreated()
        {
            // Arrange
            int categoryId = 1;

            _mockCategoryRepository
                .Setup(x => x.InsertNewCategory(It.IsAny<Category>()))
                .ReturnsAsync(Category(categoryId));

            // Act
            var result = await _categoryService.CreateCategory(CategoryRequest());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CategoryResponse>(result);
            Assert.Equal(categoryId, result.Id);
            Assert.Equal("CategoryTestName", result.Name);
            Assert.Equal("CategoryTestDescription", result.Description);
        }

        [Fact]
        public async void CreateCategory_ShouldReturnNull_WhenCategoryIsNotCreated()
        {
            // Arrange
            _mockCategoryRepository
                .Setup(x => x.InsertNewCategory(It.IsAny<Category>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _categoryService.CreateCategory(CategoryRequest());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeleteCategory_ShouldReturnCategoryResponse_WhenCategoryIsDeleted()
        {
            // Arrange
            int categoryId = 1;

            _mockCategoryRepository
                .Setup(x => x.DeleteCategoryById(It.IsAny<int>()))
                .ReturnsAsync(Category(categoryId));

            // Act
            var result = await _categoryService.DeleteCategoryById(categoryId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CategoryResponse>(result);
            Assert.Equal(categoryId, result.Id);
            Assert.Equal("CategoryTestName", result.Name);
            Assert.Equal("CategoryTestDescription", result.Description);
        }

        [Fact]
        public async void DeleteCategory_ShouldReturnNull_WhenCategoryDoesNotExist()
        {
            // Arrange
            int categoryId = 1;

            _mockCategoryRepository
                .Setup(x => x.DeleteCategoryById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _categoryService.DeleteCategoryById(categoryId);

            // Assert
            Assert.Null(result);
        }

        private Category Category(int categoryId)
        {
            return new Category
            {
                Id = categoryId,
                Name = "CategoryTestName",
                Description = "CategoryTestDescription"
            };
        }

        private List<Category> CategoryList(int categoryId1, int categoryId2)
        {
            return new List<Category>()
            {
                new Category
                {
                    Id = categoryId1,
                    Name = "CategoryTestName1",
                    Description = "CategoryTestDescription1"
                },
                new Category
                {
                    Id = categoryId2,
                    Name = "CategoryTestName2",
                    Description = "CategoryTestDescription2"
                }
            };
        }

        private CategoryRequest CategoryRequest()
        {
            return new CategoryRequest
            {
                Name = "CategoryRequestTestName",
                Description = "CategoryRequestTestDescription"
            };
        }
    }
}
