using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using T1_PR2_Client.Models;
using T1_PR2_Client.Models.DTOs;

namespace T1_PR2_Client.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public RegisterModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public RegisterUser User { get; set; }

        public string ApiErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Map User to RegisterUserDTO to avoid sending ConfirmedPassword
            var registerUserDto = new RegisterUserDTO
            {
                Name = User.Name,
                Email = User.Email,
                UserName = User.UserName,
                Password = User.Password
            };

            var baseUrl = _configuration["ApiBaseUrl"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = await client.PostAsJsonAsync("register", registerUserDto);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/Login");
                }
                else
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    ApiErrorMessage = $"Error en el registro ({response.StatusCode}): {responseBody}";
                   
                    ModelState.AddModelError(string.Empty, "Falló el registro en la API. Inténtalo de nuevo o contacta soporte.");
                    return Page();
                }
            }
        }
    }
}
