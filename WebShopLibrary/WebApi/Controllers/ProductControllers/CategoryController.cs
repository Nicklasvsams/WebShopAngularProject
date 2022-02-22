using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShopLibrary.BusinessAccessLayer.DTOs.CategoryDTO;
using WebShopLibrary.BusinessAccessLayer.Services.ProductServices;

namespace WebShopLibrary.WebApi.Controllers.ProductControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categories = await _categoryService.GetAllCategories();

                if (categories == null)
                {
                    return StatusCode(500);
                }

                if (categories.Count == 0)
                {
                    return NoContent();
                }

                return Ok(categories);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int categoryId)
        {
            try
            {
                var categoryResponse = await _categoryService.GetCategoryById(categoryId);

                if (categoryResponse == null)
                {
                    return NotFound();
                }
                return Ok(categoryResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CategoryRequest newCategory)
        {
            try
            {
                var categoryResponse = await _categoryService.CreateCategory(newCategory);

                if (categoryResponse == null)
                {
                    return StatusCode(500);
                }
                return Ok(categoryResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int categoryId, [FromBody] CategoryRequest newCategory)
        {
            try
            {
                var categoryResponse = await _categoryService.UpdateCategory(categoryId, newCategory);

                if (categoryResponse == null)
                {
                    return NotFound();
                }
                return Ok(categoryResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int categoryId)
        {
            try
            {
                var categoryResponse = await _categoryService.DeleteCategoryById(categoryId);

                if (categoryResponse == null)
                {
                    return NotFound();
                }
                return Ok(categoryResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
