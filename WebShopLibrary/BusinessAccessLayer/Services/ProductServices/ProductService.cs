using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopLibrary.BusinessAccessLayer.DTOs.ProductDTO;
using WebShopLibrary.DataAccessLayer.Database.Entities.Products;
using WebShopLibrary.DataAccessLayer.Repositories.ProductRepositories;

namespace WebShopLibrary.BusinessAccessLayer.Services.ProductServices
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetAllProducts();
        Task<ProductResponse> GetProductById(int productId);
        Task<ProductResponse> CreateProduct(ProductRequest newProduct);
        Task<ProductResponse> DeleteProduct(int productId);
        Task<ProductResponse> UpdateProduct(int productId, ProductRequest productUpdate);

    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductResponse>> GetAllProducts()
        {
            List<Product> products = await _productRepository.SelectAllProducts();

            return products.Select(x => MapProductToProductResponse(x)).ToList();
        }

        public async Task<ProductResponse> GetProductById(int productId)
        {
            Product product = await _productRepository.SelectProductById(productId);

            if(product != null)
            {
                return MapProductToProductResponse(product);
            }

            return null;
        }

        public async Task<ProductResponse> DeleteProduct(int productId)
        {
            Product product = await _productRepository.DeleteProductById(productId);

            if (product != null)
            {
                return MapProductToProductResponse(product);
            }

            return null;
        }

        public async Task<ProductResponse> CreateProduct(ProductRequest product)
        {
            Product createdProduct = await _productRepository.InsertNewProduct(MapProductRequestToProduct(product));

            if (createdProduct != null)
            {
                return MapProductToProductResponse(createdProduct);
            }

            return null;
        }

        public async Task<ProductResponse> UpdateProduct(int productId, ProductRequest product)
        {
            Product productToUpdate = await _productRepository.UpdateExistingProduct(productId, MapProductRequestToProduct(product));

            if (productToUpdate != null)
            {
                return MapProductToProductResponse(productToUpdate);
            }

            return null;
        }

        private ProductResponse MapProductToProductResponse(Product product)
        {
            return new ProductResponse()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock
            };
        }

        private Product MapProductRequestToProduct(ProductRequest prodReq)
        {
            return new Product()
            {
                Name = prodReq.Name,
                Price = prodReq.Price,
                Description = prodReq.Description,
                Stock = prodReq.Stock
            };
        }
    }
}
