using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace T1_PR2_Client.Pages
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // This triggers the cookie authentication middleware to sign out the user
            // and remove the primary authentication cookie (.AspNetCore.Cookies)
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            // Now, explicitly delete the "ApiToken" cookie which you use for API calls
            var apiTokenCookieOptions = new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(-1), // Set an expired date
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                // Add Path and Domain if they were set when creating the cookie
                // Path = "/",
                // Domain = Environment.GetEnvironmentVariable("YOUR_DOMAIN") // Example
            };

            HttpContext.Response.Cookies.Delete("authToken", apiTokenCookieOptions);

            // Optional: Add a message to TempData for the next request
            // TempData["LogoutMessage"] = "You have been successfully logged out.";

            // Redirect to the Index page after logout
            TempData["LogoutMessage"] = "You have been successfully logged out.";

            return RedirectToPage("/Index");
        }
    }
}
