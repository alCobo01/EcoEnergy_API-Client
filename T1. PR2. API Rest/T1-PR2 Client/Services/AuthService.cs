using System.Text.Json;
using System.Text;
using T1_PR2_Client.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace T1_PR2_Client.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<(bool Success, string Message)> Login(LoginUser loginModel)
        {
            // 1) Encriptar la contraseña antes de serializar
            loginModel.Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(loginModel.Password));

            // 2) Enviar las credenciales
            var content = new StringContent(
                JsonSerializer.Serialize(loginModel),
                Encoding.UTF8,
                "application/json");
            var response = await _httpClient.PostAsync("api/auth/login", content);
            var body = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                // Mensaje de error del API
                return (false, body);
            }

            // 2) 'body' es el JWT
            var token = body.Trim();

            // 3) Parsear el JWT y extraer claims
            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwt;
            try
            {
                jwt = handler.ReadJwtToken(token);
            }
            catch
            {
                return (false, "Invalid token format");
            }

            // 4) Crear identidad y principal a partir de esos claims
            var identity = new ClaimsIdentity(
                jwt.Claims,
                CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // 5) Firmar la cookie de ASP.NET
            await _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = loginModel.RememberMe,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                    AllowRefresh = true
                });

            // 6) (Opcional) guardar el token en una cookie separada
            _httpContextAccessor.HttpContext.Response.Cookies.Append(
                "authToken",
                token,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = loginModel.RememberMe
                        ? DateTimeOffset.UtcNow.AddDays(7)
                        : DateTimeOffset.UtcNow.AddHours(1)
                });

            return (true, "");
        }

        public async Task LogoutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            var apiTokenCookieOptions = new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(-1),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
            };

            _httpContextAccessor.HttpContext.Response.Cookies.Delete("ApiToken", apiTokenCookieOptions);
        }

    }
}
