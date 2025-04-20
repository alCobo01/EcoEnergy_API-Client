using Microsoft.AspNetCore.Mvc;
using T1_PR2_Client.Models;
using static System.Net.WebRequestMethods;

namespace T1_PR2_Client.Services
{
    public class GameService
    {
        private readonly HttpClient _httpClient;

        public GameService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("AuthorizedClient");
        }

        public string ApiErrorMessage { get; set; } = string.Empty;

        public async Task<List<Game>> GetGamesAsync()
        {
            var response = await _httpClient.GetAsync("api/games");
            if (response.IsSuccessStatusCode)
            {
                var games = await response.Content.ReadFromJsonAsync<List<Game>>();
                return games?
                    .OrderByDescending(g => g.RatedUsers.Count)
                    .ToList() ?? new List<Game>();
            }
            else
            {
                var body = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API Error ({response.StatusCode}): {body}");
            }
        }

        public async Task<Game> GetGameByIdAsync(int gameId)
        {
            var response = await _httpClient.GetAsync($"api/games/{gameId}");
            if (response.IsSuccessStatusCode)
            {
                var game = await response.Content.ReadFromJsonAsync<Game>();
                return game;
            }
            else
            {
                var body = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"API Error ({response.StatusCode}): {body}");
            }
        }

        public async Task<bool> VoteAsync(int gameId, string username)
        {
            var url = $"api/games/vote?gameId={gameId}&userName={username}";
            var response = await _httpClient.PostAsync(url, null);
            return response.IsSuccessStatusCode;
        }

        
    }
}
