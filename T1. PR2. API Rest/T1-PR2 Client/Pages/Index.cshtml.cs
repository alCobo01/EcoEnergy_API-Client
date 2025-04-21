using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using T1_PR2_Client.Models;
using T1_PR2_Client.Services;

namespace T1_PR2_Client.Pages;

public class IndexModel : PageModel
{
    private readonly IConfiguration _configuration;
    private readonly GameService _gameService;

    public IndexModel(IConfiguration configuration, GameService gameService)
    {
        _configuration = configuration;
        _gameService = gameService;
    }

    public List<Game> Games { get; set; } = new();

    public string ApiErrorMessage { get; set; } = string.Empty;

    public async Task<IActionResult> OnGetAsync()
    {

        try
        {
            Games = await _gameService.GetGamesAsync();
        }
        catch (HttpRequestException ex)
        {
            ApiErrorMessage = ex.Message;
            ModelState.AddModelError(string.Empty, ApiErrorMessage);
        }
        catch (Exception ex)
        {
            ApiErrorMessage = $"Exception calling API: {ex.Message}";
            ModelState.AddModelError(string.Empty, ApiErrorMessage);
        }

        return Page();
    }

    public async Task<IActionResult> OnPostVoteAsync(int gameId)
    {
        var username = User.Identity?.Name;
        if (string.IsNullOrEmpty(username))
        {
            ApiErrorMessage = "You have to be logged in to vote.";
            await OnGetAsync();
            return Page();
        }

        var game = await _gameService.GetGameByIdAsync(gameId);
        bool alreadyVoted = game.RatedUsers.Contains(username);

        var success = await _gameService.VoteAsync(gameId, username);
        if (!success)
        {
            ApiErrorMessage = "Error voting. Try later or contact support.";
        } else
        {
            if (alreadyVoted)
            {
                TempData["SuccessMessage"] = $"Vote for game {game.Title} removed successfully!";
            }
            else
            {
                TempData["SuccessMessage"] = $"Vote for game {game.Title} registered successfully!";
            }
        }

        await OnGetAsync();
        return Page();
    }
}
