using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using T1_PR2_Client.Models;
using T1_PR2_Client.Services;
using System.Linq;

namespace T1_PR2_Client.Pages
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly GameService _gameService;
        private readonly HttpClient _httpClient;

        public ProfileModel(GameService gameService, IHttpClientFactory httpClientFactory)
        {
            _gameService = gameService;
            _httpClient = httpClientFactory.CreateClient("AuthorizedClient");
        }

        public UserProfile User { get; set; }
        public List<Game> FavoriteGames { get; set; } = new List<Game>();
        public int UserGameVotes { get; set; }
        public int UserComments { get; set; }
        public bool IsAdmin { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Si l'usuari no està autenticat, redirigir a la pàgina de login
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Login");
            }

            // Obtenir username de les claims
            var username = User.Identity.Name;
            
            try
            {
                // Aquí simularé les dades d'usuari, en un entorn real s'obtindrien de l'API
                User = new UserProfile
                {
                    UserName = username,
                    Name = User.FindFirst(ClaimTypes.Name)?.Value ?? username,
                    Email = User.FindFirst(ClaimTypes.Email)?.Value ?? $"{username}@example.com",
                    RegistrationDate = DateTime.Now.AddMonths(-3)  // Simulat
                };

                // Comprovar si l'usuari és administrador
                IsAdmin = User.IsInRole("Admin");

                // Obtenir els jocs favorits de l'usuari (els que ha votat)
                var allGames = await _gameService.GetGamesAsync();
                FavoriteGames = allGames.Where(g => g.RatedUsers.Contains(username)).ToList();

                // Estadístiques simulades
                UserGameVotes = FavoriteGames.Count;
                UserComments = new Random().Next(5, 25);  // Valor aleatori per simular missatges de chat

                return Page();
            }
            catch (Exception ex)
            {
                // Registrar l'error i mostrar la pàgina amb un missatge d'error
                Console.WriteLine($"Error loading profile: {ex.Message}");
                return Page();
            }
        }
    }

    // Classe per representar el perfil d'usuari
    public class UserProfile
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
} 