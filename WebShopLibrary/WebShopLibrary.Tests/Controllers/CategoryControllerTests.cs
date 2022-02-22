using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using WebShopLibrary.BusinessAccessLayer.DTOs.CategoryDTO;
using WebShopLibrary.BusinessAccessLayer.Services.ProductServices;
using WebShopLibrary.WebApi.Controllers.ProductControllers;
using Xunit;

namespace WebShopLibrary.Tests.Controllers
{
    public class CategoryControllerTests
    {
        private readonly CategoryController _categoryController;
        private readonly Mock<ICategoryService> _mockCategoryService = new Mock<ICategoryService>();

        public CategoryControllerTests()
        {
            _categoryController = new CategoryController(_mockCategoryService.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_WhenNoCategoriesExist()
        {
            // Arrange
            List<CategoryResponse> categories = new List<CategoryResponse>();

            _mockCategoryService
                .Setup(x => x.GetAllCategories())
                .ReturnsAsync(categories);

            // Act
            var result = await _categoryController.GetAll();
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenCategoriesExist()
        {
            // Arrange
            List<CategoryResponse> categories = new List<CategoryResponse>();

            categories.AddRange(CategoryResponseList());

            _mockCategoryService
                .Setup(x => x.GetAllCategories())
                .ReturnsAsync(categories);

            // Act
            var result = await _categoryController.GetAll();
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenServiceReturnsNull()
        {
            // Arrange
            _mockCategoryService
                .Setup(x => x.GetAllCategories())
                .ReturnsAsync(() => null);

            // Act
            var result = await _categoryController.GetAll();
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _mockCategoryService
                .Setup(x => x.GetAllCategories())
                .ReturnsAsync(() => throw new Exception("test"));

            // Act
            var result = await _categoryController.GetAll();
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode200_WhenCategoryExists()
        {
            // Arrange
            int categoryId = 1;

            _mockCategoryService
                .Setup(x => x.GetCategoryById(It.IsAny<int>()))
                .ReturnsAsync(CategoryResponse());

            // Act
            var result = await _categoryController.GetById(categoryId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int categoryId = 1;

            _mockCategoryService
                .Setup(x => x.GetCategoryById(It.IsAny<int>()))
                .ReturnsAsync(() => throw new Exception("Test"));

            // Act
            var result = await _categoryController.GetById(categoryId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenCategoryDoesNotExist()
        {
            // Arrange
            int categoryId = 1;

            _mockCategoryService
                .Setup(x => x.GetCategoryById(categoryId))
                .ReturnsAsync(() => null);

            // Act
            var result = await _categoryController.GetById(categoryId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode200_WhenCategoryIsCreated()
        {
            // Arrange
            _mockCategoryService
                .Setup(x => x.CreateCategory(It.IsAny<CategoryRequest>()))
                .ReturnsAsync(CategoryResponse());

            // Act
            var result = await _categoryController.Create(CategoryRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _mockCategoryService
                .Setup(x => x.CreateCategory(It.IsAny<CategoryRequest>()))
                .ReturnsAsync(() => throw new Exception("Test"));

            // Act
            var result = await _categoryController.Create(CategoryRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenUpdateIsSuccessful()
        {
            // Arrange
            int categoryId = 1;

            _mockCategoryService
                .Setup(x => x.UpdateCategory(It.IsAny<int>(), It.IsAny<CategoryRequest>()))
                .ReturnsAsync(CategoryResponse());

            // Act
            var result = await _categoryController.Update(categoryId, CategoryRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode404_WhenCategoryToUpdateIsNotFound()
        {
            // Arrange
            int categoryId = 1;

            _mockCategoryService
                .Setup(x => x.UpdateCategory(It.IsAny<int>(), It.IsAny<CategoryRequest>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _categoryController.Update(categoryId, CategoryRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int categoryId = 1;

            _mockCategoryService
                .Setup(x => x.UpdateCategory(It.IsAny<int>(), It.IsAny<CategoryRequest>()))
                .ReturnsAsync(() => throw new Exception("Test"));

            // Act
            var result = await _categoryController.Update(categoryId, CategoryRequest());
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode200_WhenDeleteIsSuccessful()
        {
            // Arrange
            int categoryId = 1;

            _mockCategoryService
                .Setup(x => x.DeleteCategoryById(It.IsAny<int>()))
                .ReturnsAsync(CategoryResponse());

            // Act
            var result = await _categoryController.Delete(categoryId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode404_WhenCategoryToDeleteIsNotFound()
        {
            // Arrange
            int categoryId = 1;

            _mockCategoryService
                .Setup(x => x.DeleteCategoryById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _categoryController.Delete(categoryId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int categoryId = 1;

            _mockCategoryService
                .Setup(x => x.DeleteCategoryById(It.IsAny<int>()))
                .ReturnsAsync(() => throw new Exception("Test"));

            // Act
            var result = await _categoryController.Delete(categoryId);
            var statusCodeResult = (IStatusCodeActionResult)result;

            // Assert
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        private CategoryResponse CategoryResponse()
        {
            return new CategoryResponse()
            {
                Id = 1,
                Name = "CategoryResponseTestName",
                Description = "CategoryResponseTestDescription"
            };
        }

        private CategoryRequest CategoryRequest()
        {
            return new CategoryRequest()
            {
                Name = "CategoryResponseTestName",
                Description = "CategoryResponseTestDescription"
            };
        }

        private List<CategoryResponse> CategoryResponseList()
        {
            return new List<CategoryResponse>()
            {
                new CategoryResponse()
                {
                    Id = 1,
                    Name = "CategoryResponseTestName1",
                    Description = "CategoryResponseTestDescription1"
                },
                new CategoryResponse()
                {
                    Id = 2,
                    Name = "CategoryResponseTestName2",
                    Description = "CategoryResponseTestDescription2"
                }
            };
        }
    }
}
