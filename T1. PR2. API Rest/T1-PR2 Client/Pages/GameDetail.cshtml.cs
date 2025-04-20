using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using T1_PR2_Client.Models;
using T1_PR2_Client.Services;

namespace T1_PR2_Client.Pages
{
    public class GameDetailModel : PageModel
    {
        public readonly GameService _gameService;

        public GameDetailModel(GameService gameService)
        {
            _gameService = gameService;
        }

        public Game Game { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Game = await _gameService.GetGameByIdAsync(id);
            return Page();
        }
    }
}
