using Games_ASPNET.Exceptions;
using Games_ASPNET.InputModel;
using Games_ASPNET.Services;
using Games_ASPNET.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Games_ASPNET.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> Get([FromQuery, Range(1, int.MaxValue)] int page=1, [FromQuery, Range(1, 50)] int qtt=5)
        {
            var result = await _gameService.Get(page, qtt);
            if(result.Count() == 0)
                return NoContent();
            return Ok(result);
        }

        [HttpGet("{idGame:guid}")]
        public async Task<ActionResult<List<GameViewModel>>> Get([FromRoute] Guid idGame)
        {
            var result = await _gameService.Get(idGame);
            if (result == null)
                return NoContent();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<GameViewModel>>> Insert([FromBody] GameInputModel game)
        {
            try
            {
                var result = await _gameService.Insert(game);
                return Ok(result);
            }
            catch(GameAlreadyRegisteredException e)
            {
                return UnprocessableEntity("There is already a game with that name by that developer");
            }
        }

        [HttpPut("{idGame:guid}")]
        public async Task<ActionResult> Update([FromRoute] Guid idGame, [FromBody] GameInputModel game)
        {
            try
            {
                await _gameService.Update(idGame, game);
                return Ok();
            }
            catch(GameNotRegisteredException e)
            {
                return NotFound("This game does not exist");
            }
        }

        [HttpPatch("{idGame:guid}/price/{price:double}")]
        public async Task<ActionResult> Update([FromRoute] Guid idGame, [FromRoute] double price)
        {
            try
            {
                await _gameService.Update(idGame, price);
                return Ok();
            }
            catch(GameNotRegisteredException e)
            {
                return NotFound("This game does not exist");
            }
        }

        [HttpDelete("{idGame:Guid}")]
        public async Task<ActionResult> Delete([FromRoute] Guid idGame)
        {
            try
            {
                await _gameService.Delete(idGame);
                return Ok();
            }
            catch(GameNotRegisteredException e)
            {
                return NotFound("This game does not exist");
            }
        }
    }
}
