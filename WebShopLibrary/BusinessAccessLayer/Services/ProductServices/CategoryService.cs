using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopLibrary.BusinessAccessLayer.DTOs.CategoryDTO;
using WebShopLibrary.DataAccessLayer.Database.Entities.Products;
using WebShopLibrary.DataAccessLayer.Repositories.ProductRepositories;

namespace WebShopLibrary.BusinessAccessLayer.Services.ProductServices
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAllCategories();
        Task<CategoryResponse> GetCategoryById(int categoryId);
        Task<CategoryResponse> DeleteCategoryById(int categoryId);
        Task<CategoryResponse> CreateCategory(CategoryRequest newCategory);
        Task<CategoryResponse> UpdateCategory(int categoryId, CategoryRequest categoryUpdate);
    }

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryResponse> CreateCategory(CategoryRequest newCategory)
        {
            Category category = MapCategoryRequestToCategory(newCategory);

            Category insertedCategory = await _categoryRepository.InsertNewCategory(category);

            if (insertedCategory != null)
            {
                return MapCategoryToCategoryResponse(insertedCategory);
            }

            return null;
        }

        public async Task<CategoryResponse> DeleteCategoryById(int categoryId)
        {
            Category deletedCategory = await _categoryRepository.DeleteCategoryById(categoryId);

            if (deletedCategory != null)
            {
                return MapCategoryToCategoryResponse(deletedCategory);
            }

            return null;
        }

        public async Task<List<CategoryResponse>> GetAllCategories()
        {
            List<Category> categories = await _categoryRepository.SelectAllCategories();

            return categories.Select(category => MapCategoryToCategoryResponse(category)).ToList();
        }

        public async Task<CategoryResponse> GetCategoryById(int categoryId)
        {
            Category categoryById = await _categoryRepository.SelectCategoryById(categoryId);

            if (categoryById != null)
            {
                return MapCategoryToCategoryResponse(categoryById);
            }

            return null;
        }

        public async Task<CategoryResponse> UpdateCategory(int categoryId, CategoryRequest categoryUpdate)
        {
            Category categoryToUpdate = await _categoryRepository.UpdateExistingCategory(categoryId, MapCategoryRequestToCategory(categoryUpdate));

            if (categoryToUpdate != null)
            {
                return MapCategoryToCategoryResponse(categoryToUpdate);
            }

            return null;
        }

        private static CategoryResponse MapCategoryToCategoryResponse(Category category)
        {
            CategoryResponse catRes = new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };

            return catRes;
        }

        private static Category MapCategoryRequestToCategory(CategoryRequest categoryRequest)
        {
            return new Category
            {
                Name = categoryRequest.Name,
                Description = categoryRequest.Description
            };
        }
    }
}
