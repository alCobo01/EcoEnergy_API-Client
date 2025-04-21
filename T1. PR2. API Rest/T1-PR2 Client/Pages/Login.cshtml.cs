using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using T1_PR2_Client.Models;
using T1_PR2_Client.Services;

namespace T1_PR2_Client.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AuthService _authService;

        public LoginModel(AuthService authService)
        {
            _authService = authService;
        }

        [BindProperty]
        public LoginUser User { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public void OnGet()
        {
            TempData.Clear();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (!ModelState.IsValid)
                return Page();

            var result = await _authService.Login(User);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return Page();
            }

            TempData["SuccessMessage"] = "Login successful! Redirecting...";
            return Page();
        }

    }
}
