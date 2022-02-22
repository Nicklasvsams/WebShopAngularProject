using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebShopLibrary.DataAccessLayer.Database;
using WebShopLibrary.DataAccessLayer.Database.Entities.Products;

namespace WebShopLibrary.DataAccessLayer.Repositories.ProductRepositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> SelectAllCategories();
        Task<Category> SelectCategoryById(int categoryId);
        Task<Category> DeleteCategoryById(int categoryId);
        Task<Category> UpdateExistingCategory(int categoryId, Category category);
        Task<Category> InsertNewCategory(Category category);
    }

    public class CategoryRepository : ICategoryRepository
    {
        private readonly WebShopDBContext _dbContext;

        public CategoryRepository(WebShopDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Category> DeleteCategoryById(int categoryId)
        {
            Category categoryToDelete = await _dbContext.Category.FirstOrDefaultAsync(x => x.Id == categoryId);

            if (categoryToDelete != null)
            {
                _dbContext.Remove(categoryToDelete);

                await _dbContext.SaveChangesAsync();
            }

            return categoryToDelete;
        }

        public async Task<Category> InsertNewCategory(Category category)
        {
            await _dbContext.Category.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<List<Category>> SelectAllCategories()
        {
            return await _dbContext.Category.ToListAsync();
        }

        public async Task<Category> SelectCategoryById(int categoryId)
        {
            return await _dbContext.Category.FirstOrDefaultAsync(x => x.Id == categoryId);
        }

        public async Task<Category> UpdateExistingCategory(int categoryId, Category category)
        {
            Category categoryToUpdate = await _dbContext.Category.FirstOrDefaultAsync(x => x.Id == categoryId);

            if (categoryToUpdate != null)
            {
                categoryToUpdate.Name = category.Name;
                categoryToUpdate.Description = category.Description;

                await _dbContext.SaveChangesAsync();
            }

            return categoryToUpdate;
        }
    }
}
