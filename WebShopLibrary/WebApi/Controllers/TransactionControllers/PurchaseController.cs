using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebShopLibrary.BusinessAccessLayer.DTOs.PurchaseDTO;
using WebShopLibrary.BusinessAccessLayer.Services.TransactionServices;

namespace WebShopLibrary.WebApi.Controllers.TransactionControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var purchaseResponses = await _purchaseService.GetAllPurchases();

                if (purchaseResponses == null)
                {
                    return StatusCode(500);
                }

                if (purchaseResponses.Count == 0)
                {
                    return NoContent();
                }

                return Ok(purchaseResponses);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{purchaseId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int purchaseId)
        {
            try
            {
                var purchaseResponse = await _purchaseService.GetPurchaseById(purchaseId);

                if (purchaseResponse == null)
                {
                    return NotFound();
                }
                return Ok(purchaseResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] PurchaseRequest purchaseRequest)
        {
            try
            {
                var purchaseResponse = await _purchaseService.CreatePurchase(purchaseRequest);

                if (purchaseResponse == null)
                {
                    return StatusCode(500);
                }
                return Ok(purchaseResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{purchaseId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int purchaseId, [FromBody] PurchaseRequest purchaseRequest)
        {
            try
            {
                var purchaseResponse = await _purchaseService.UpdatePurchase(purchaseId, purchaseRequest);

                if (purchaseResponse == null)
                {
                    return NotFound();
                }
                return Ok(purchaseResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{purchaseId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int purchaseId)
        {
            try
            {
                var purchaseResponse = await _purchaseService.DeletePurchase(purchaseId);

                if (purchaseResponse == null)
                {
                    return NotFound();
                }
                return Ok(purchaseResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
