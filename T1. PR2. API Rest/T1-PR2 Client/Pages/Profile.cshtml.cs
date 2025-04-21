using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using T1_PR2_Client.Models;
using T1_PR2_Client.Services;

namespace T1_PR2_Client.Pages
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly GameService _gameService;

        public ProfileModel(GameService gameService, IHttpClientFactory httpClientFactory)
        {
            _gameService = gameService;
        }

        public List<Game> FavoriteGames { get; set; } = new List<Game>();
        public int UserComments { get; set; }
        public bool IsAdmin { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!HttpContext.User.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToPage("/Login");
            }

            var username = HttpContext.User.Identity.Name;

            try
            {
                var allGames = await _gameService.GetGamesAsync();
                FavoriteGames = allGames.Where(g => g.RatedUsers.Contains(username)).ToList();

                return Page();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading profile: {ex.Message}");
                return Page();
            }
        }
    }
} 