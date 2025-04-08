using Microsoft.AspNetCore.Authorization;
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
            var games = await _context.Games
                .Include(g => g.RatedUsers)
                .ToListAsync();

            if (games.Count == 0) return NotFound("No games found!");

            var gameDTOs = games.Select(g => new GetGameDTO
            {
                Id = g.Id,
                Title = g.Title,
                Description = g.Description,
                DeveloperTeam = g.DeveloperTeam,
                RatedUsers = g.RatedUsers.Select(u => u.UserName).ToList()
            }).ToList();

            
            return Ok(gameDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetById(int id)
        {
            var game = await _context.Games.FirstOrDefaultAsync(g => g.Id == id);
            if (game == null) return NotFound($"Game {id} not found!");
            return Ok(game);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Game>> Add(InsertGameDTO gameDTO)
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

                return CreatedAtAction(nameof(GetById), new { id = game.Id } , game);
            }
            catch (DbUpdateException)
            {
                return BadRequest("An error occurred while adding the game. Try again.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
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

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Game>> Update(int id, InsertGameDTO gameDTO)
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

        [Authorize]
        [HttpPost("vote")]
        public async Task<ActionResult> Vote(int gameId, string userId)
        {
            try
            {
                var game = await _context.Games.Include(g => g.RatedUsers).FirstOrDefaultAsync(g => g.Id == gameId);
                if (game == null) return NotFound($"Game {gameId} not found!");

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (user == null) return NotFound($"User {userId} not found!");

                if (game.RatedUsers.Any(u => u.Id == userId)) return BadRequest("User has already voted for this game.");

                game.RatedUsers.Add((User)user);
                await _context.SaveChangesAsync();

                return Ok($"Game {game.Title} upvoted!");
            }
            catch (DbUpdateException)
            {
                return BadRequest("An error occurred while voting. Try again.");
            }
        }
    }
}
