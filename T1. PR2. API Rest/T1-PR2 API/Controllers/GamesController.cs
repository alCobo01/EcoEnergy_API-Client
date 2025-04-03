using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T1_PR2_API.Data;
using T1_PR2_API.DTOs;
using T1_PR2_API.Models;

namespace T1_PR2_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GamesController(AppDbContext context) { _context = context; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetAll()
        {
            var films = await _context.Games.ToListAsync();
            if (films.Count == 0) return NotFound("No games found!");
            return Ok(films);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetByName(string title)
        {
            var game = await _context.Games.FirstOrDefaultAsync(g => g.Title == title);
            if (game == null) return NotFound($"Game {title} not found!");
            return Ok(game);
        }

        //Falta autenticació per afegir, editar i borrar
        [HttpPost]
        public async Task<ActionResult<Game>> Add(GameDTO gameDTO)
        {
            try
            {
                if (gameDTO == null) return BadRequest("Game data is required!");

                var existingGame = await _context.Games.FirstOrDefaultAsync(g => g.Title == gameDTO.Title);
                if (existingGame != null) return Conflict($"Game with title {gameDTO.Title} already exists!");

                var game = new Game
                {
                    Title = gameDTO.Title,
                    Description = gameDTO.Description,
                    DeveloperTeam = gameDTO.DeveloperTeam
                };

                _context.Games.Add(game);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetByName), game);
            }
            catch (DbUpdateException)
            {
                return BadRequest("An error occurred while adding the game. Try again.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Game>> Delete(int id)
        {
            try
            {
                var game = await _context.Games.FirstOrDefaultAsync(g => g.Id == id);
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
                return Ok(game);
            }
            catch (DbUpdateException)
            {
                return BadRequest($"An error ocurred while deleting the game with id {id}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Game>> Update(int id, GameDTO gameDTO)
        {
            try
            {
                var game = await _context.Games.FirstOrDefaultAsync(g => g.Id == id);
                game.Title = gameDTO.Title;
                game.Description = gameDTO.Description;
                game.DeveloperTeam = gameDTO.DeveloperTeam;
                await _context.SaveChangesAsync();
                return Ok(game);
            }
            catch (DbUpdateException)
            {
                return BadRequest("An error ocurred while updating the game. Try again.");
            }
        }




    }
}
