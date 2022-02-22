using BusinessAccessLayer.Services.GameServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebShopLibrary.BusinessAccessLayer.DTOs.GameDTO;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var games = await _gameService.GetAllGames();

                if (games == null)
                {
                    return StatusCode(500);
                }

                if (games.Count == 0)
                {
                    return NoContent();
                }

                return Ok(games);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{gameId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute]int gameId)
        {
            try
            {
                var gameResponse = await _gameService.GetGameById(gameId);

                if (gameResponse == null)
                {
                    return NotFound();
                }
                return Ok(gameResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] GameRequest newGame)
        {
            try
            {
                var gameResponse = await _gameService.CreateGame(newGame);

                if (gameResponse == null)
                {
                    return StatusCode(500);
                }
                return Ok(gameResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{gameId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int gameId, [FromBody] GameRequest newGame)
        {
            try
            {
                var gameResponse = await _gameService.UpdateGame(gameId, newGame);

                if (gameResponse == null)
                {
                    return NotFound();
                }
                return Ok(gameResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{gameId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int gameId)
        {
            try
            {
                var gameResponse = await _gameService.DeleteGame(gameId);

                if (gameResponse == null)
                {
                    return NotFound();
                }
                return Ok(gameResponse);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
