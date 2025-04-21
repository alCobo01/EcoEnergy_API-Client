using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using T1_PR2_Client.Models;
using T1_PR2_Client.Models.DTOs;

namespace T1_PR2_Client.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public RegisterModel(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
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

            var response = await _httpClient.PostAsJsonAsync("api/auth/register", registerUserDto);
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Registration successful! Redirecting to log in...";
                TempData["RegisterSuccess"] = true;
                return Page();
            }
            else
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                ApiErrorMessage = $"Error creating account: ({response.StatusCode}): {responseBody}";

                ModelState.AddModelError(string.Empty, ApiErrorMessage);
                return Page();
            }
        }
    }
}
