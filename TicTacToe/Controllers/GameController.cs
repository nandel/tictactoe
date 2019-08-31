using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Concurrent;
using TicTacToe.Domain.Entities;
using TicTacToe.Dtos;

namespace TicTacToe.Controllers
{
    [Route("game")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly static ConcurrentDictionary<Guid, Game> _database
            = new ConcurrentDictionary<Guid, Game>();

        [HttpPost]
        public IActionResult Create()
        {
            // Criamos o novo game e tentamos adicionar à base
            var game = new Game();            
            if (_database.TryAdd(game.Id, game))
            {
                return Ok(new
                {
                    id = game.Id,
                    firstPlayer = game.FirstPlayer.ToString()
                });
            }

            // Caso a criação do game falhe
            return StatusCode(500);
        }

        [HttpPost("{id}/movement")]
        public IActionResult AddMovement([FromRoute] string id, [FromBody]CreateMovementDto input)
        {
            try
            {
                if (Guid.TryParse(id, out var guid) && _database.TryGetValue(guid, out var game))
                {
                    // Se o jogo ja tiver finalizado antes da jogada
                    // pela doc, não da ideia que um erro deveria ser retornado
                    // mas sim o resultado do jogo
                    if (game.IsFinish)
                    {
                        return ReturnGameResult(game);
                    }                    

                    // fazemos o movimento
                    var move = game.MakeAMove(input.Player, input.Position.X, input.Position.Y);
                    
                    // Se o jogo terminar após jogada
                    if (game.IsFinish)
                    {
                        return ReturnGameResult(game);
                    }

                    return Ok(new
                    {
                        msg = "Movimento realizado"
                    }); 
                }

                return NotFound(new
                {
                    msg = "Partida não encontrada"
                });
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    msg = ex.Message
                });
            }            
        }
        private IActionResult ReturnGameResult(Game game)
        {
            return Ok(new
            {
                msg = "Partida finalizada",
                winner = game.Winner.HasValue ? game.Winner.Value.ToString() : "Draw"
            });
        }
    }    
}