using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using T1_PR2_Client.Models;
using T1_PR2_Client.Models.DTOs;

namespace T1_PR2_Client.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public User User { get; set; }

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

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7003/api/auth/");
                var response = await client.PostAsJsonAsync("register", registerUserDto);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/Index");
                }
                else
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    ApiErrorMessage = $"Error en el registro ({response.StatusCode}): {responseBody}";
                   
                    ModelState.AddModelError(string.Empty, "Falló el registro en la API. Inténtalo de nuevo o contacta soporte."); // Mensaje genérico al usuario
                    return Page();
                }
            }
        }
    }
}
