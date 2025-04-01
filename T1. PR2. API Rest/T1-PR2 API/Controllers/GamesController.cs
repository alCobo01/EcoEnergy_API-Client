using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T1_PR2_API.Data;
using T1_PR2_API.Models;

namespace T1_PR2_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IdentityDbContext _context;

        public GamesController(IdentityDbContext context) { _context = context; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetAll()
        {
            var films = await _context.Games.ToListAsync();
            if (films.Count == 0) return NotFound("No gamess found!");
            return Ok(films);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Game>> GetByName(string name)
        {
            var game = await _context.Games.FirstOrDefaultAsync(g => g.Title == name);
            if (game == null) return NotFound($"Game {name} not found!");
            return Ok(game);
        }
    }
}
