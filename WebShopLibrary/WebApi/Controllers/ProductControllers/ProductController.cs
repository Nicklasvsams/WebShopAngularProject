using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebShopLibrary.BusinessAccessLayer.DTOs.ProductDTO;
using WebShopLibrary.BusinessAccessLayer.Services.ProductServices;

namespace WebShopLibrary.WebApi.Controllers.ProductControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var products = await _productService.GetAllProducts();

                if (products == null)
                {
                    return StatusCode(500);
                }

                if (products.Count == 0)
                {
                    return NoContent();
                }

                return Ok(products);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int productId)
        {
            try
            {
                var productResponse = await _productService.GetProductById(productId);

                if (productResponse == null)
                {
                    return NotFound();
                }
                return Ok(productResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ProductRequest newProduct)
        {
            try
            {
                var productResponse = await _productService.CreateProduct(newProduct);

                if (productResponse == null)
                {
                    return StatusCode(500);
                }
                return Ok(productResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int productId, [FromBody] ProductRequest productRequest)
        {
            try
            {
                var productResponse = await _productService.UpdateProduct(productId, productRequest);

                if (productResponse == null)
                {
                    return NotFound();
                }
                return Ok(productResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int productId)
        {
            try
            {
                var productResponse = await _productService.DeleteProduct(productId);

                if (productResponse == null)
                {
                    return NotFound();
                }
                return Ok(productResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
