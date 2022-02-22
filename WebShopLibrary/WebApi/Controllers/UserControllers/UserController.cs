using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebShopLibrary.BusinessAccessLayer.DTOs.UserDTO;
using WebShopLibrary.BusinessAccessLayer.Services.UserServices;

namespace WebShopLibrary.WebApi.Controllers.UserControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var userResponses = await _userService.GetAllUsers();

                if (userResponses == null)
                {
                    return StatusCode(500);
                }

                if (userResponses.Count == 0)
                {
                    return NoContent();
                }

                return Ok(userResponses);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] int userId)
        {
            try
            {
                var userResponse = await _userService.GetUserById(userId);

                if (userResponse == null)
                {
                    return NotFound();
                }
                return Ok(userResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] UserRequest userRequest)
        {
            try
            {
                var userResponse = await _userService.CreateUser(userRequest);

                if (userResponse == null)
                {
                    return StatusCode(500);
                }
                return Ok(userResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int userId, [FromBody] UserRequest userRequest)
        {
            try
            {
                var userResponse = await _userService.UpdateUser(userId, userRequest);

                if (userResponse == null)
                {
                    return NotFound();
                }
                return Ok(userResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int userId)
        {
            try
            {
                var userResponse = await _userService.DeleteUser(userId);

                if (userResponse == null)
                {
                    return NotFound();
                }
                return Ok(userResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
