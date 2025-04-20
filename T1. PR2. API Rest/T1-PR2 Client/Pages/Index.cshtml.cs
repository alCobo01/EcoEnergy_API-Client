using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using T1_PR2_Client.Models;

namespace T1_PR2_Client.Pages;

public class IndexModel : PageModel
{
    private readonly IConfiguration _configuration;
    public IndexModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public List<Game> Games { get; set; } = new();

    public string ApiErrorMessage { get; set; } = string.Empty;

    public async Task<IActionResult> OnGetAsync()
    {
        var baseUrl = _configuration["ApiBaseUrl"];
        try
        {
            using var client = new HttpClient() { };
            client.BaseAddress = new Uri(baseUrl);

            var response = await client.GetAsync("api/games");
            if (response.IsSuccessStatusCode)
            {
                var gamesFromApi = await response.Content.ReadFromJsonAsync<List<Game>>();
                if (gamesFromApi != null)
                    Games = gamesFromApi
                        .OrderByDescending(g => g.RatedUsers.Count)
                        .ToList();
            }
            else
            {
                var body = await response.Content.ReadAsStringAsync();
                ApiErrorMessage = $"Error retrieving games: ({response.StatusCode}): {body}";
                ModelState.AddModelError(string.Empty, ApiErrorMessage);
            }
        }
        catch (Exception ex)
        {
            ApiErrorMessage = $"Exception calling API: {ex.Message}";
            ModelState.AddModelError(string.Empty, ApiErrorMessage);
        }

        return Page();
    }
}
