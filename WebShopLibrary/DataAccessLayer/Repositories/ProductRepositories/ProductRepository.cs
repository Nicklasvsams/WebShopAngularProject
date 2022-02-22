using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebShopLibrary.DataAccessLayer.Database;
using WebShopLibrary.DataAccessLayer.Database.Entities.Products;

namespace WebShopLibrary.DataAccessLayer.Repositories.ProductRepositories
{
    public interface IProductRepository
    {
        Task<List<Product>> SelectAllProducts();
        Task<Product> SelectProductById(int productId);
        Task<Product> InsertNewProduct(Product product);
        Task<Product> UpdateExistingProduct(int productId, Product product);
        Task<Product> DeleteProductById(int productId);
    }


    public class ProductRepository : IProductRepository
    {
        private readonly WebShopDBContext _dbContext;

        public ProductRepository(WebShopDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> DeleteProductById(int productId)
        {
            Product productToDelete = await _dbContext.Product.FirstOrDefaultAsync(x => x.Id == productId);

            if (productToDelete != null)
            {
                _dbContext.Product.Remove(productToDelete);

                await _dbContext.SaveChangesAsync();
            }

            return productToDelete;
        }

        public async Task<Product> InsertNewProduct(Product product)
        {
            await _dbContext.Product.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return product;
        }

        public async Task<List<Product>> SelectAllProducts()
        {
            return await _dbContext.Product
                .ToListAsync();
        }

        public async Task<Product> SelectProductById(int productId)
        {
            return await _dbContext.Product
                .FirstOrDefaultAsync(x => x.Id == productId);
        }

        public async Task<Product> UpdateExistingProduct(int productId, Product product)
        {
            Product productToUpdate = await _dbContext.Product.FirstOrDefaultAsync(x => x.Id == productId);

            if (productToUpdate != null)
            {
                productToUpdate.Name = product.Name;
                productToUpdate.Price = product.Price;
                productToUpdate.Description = product.Description;
                productToUpdate.Stock = product.Stock;

                await _dbContext.SaveChangesAsync();
            }

            return productToUpdate;
        }
    }
}
