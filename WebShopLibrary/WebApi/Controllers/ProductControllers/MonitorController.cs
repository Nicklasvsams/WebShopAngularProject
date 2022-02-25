using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebShopLibrary.BusinessAccessLayer.DTOs.MonitorDTO;
using WebShopLibrary.BusinessAccessLayer.Services.ProductServices;

namespace WebShopLibrary.WebApi.Controllers.ProductControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitorController : ControllerBase
    {
        private readonly IMonitorService _monitorService;

        public MonitorController(IMonitorService monitorService)
        {
            _monitorService = monitorService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var monitors = await _monitorService.GetAllMonitors();

                if (monitors == null)
                {
                    return StatusCode(500);
                }

                if (monitors.Count == 0)
                {
                    return NoContent();
                }

                return Ok(monitors);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{monitorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int monitorId)
        {
            try
            {
                var monitorResponse = await _monitorService.GetMonitorById(monitorId);

                if (monitorResponse == null)
                {
                    return NotFound();
                }
                return Ok(monitorResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] MonitorRequest newMonitor)
        {
            try
            {
                var monitorResponse = await _monitorService.CreateMonitor(newMonitor);

                if (monitorResponse == null)
                {
                    return StatusCode(500);
                }
                return Ok(monitorResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{monitorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int monitorId, [FromBody] MonitorRequest newMonitor)
        {
            try
            {
                var monitorResponse = await _monitorService.UpdateMonitor(monitorId, newMonitor);

                if (monitorResponse == null)
                {
                    return NotFound();
                }
                return Ok(monitorResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{monitorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int monitorId)
        {
            try
            {
                var monitorResponse = await _monitorService.DeleteMonitor(monitorId);

                if (monitorResponse == null)
                {
                    return NotFound();
                }
                return Ok(monitorResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
