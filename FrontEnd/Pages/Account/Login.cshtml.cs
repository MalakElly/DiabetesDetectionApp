using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;
using System.Text.Json;

namespace FrontEnd.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly HttpClient _http;

        public string ErrorMessage { get; set; } = string.Empty;
        public LoginModel(IHttpClientFactory httpClientFactory)
        {
            _http = httpClientFactory.CreateClient("AuthService");
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var response = await _http.PostAsJsonAsync("api/auth/login", Input);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                //stocker le token JWT en session,
                HttpContext.Session.SetString("JwtToken", result.Token);
                return RedirectToPage("/Home/Index");
            }

            var content = await response.Content.ReadAsStringAsync();

            try
            {
                var error = JsonSerializer.Deserialize<Dictionary<string, string>>(content);
                ErrorMessage = error?["message"] ?? "Erreur inconnue";
            }
            catch
            {
                ErrorMessage = "Mot de passe invalide";
            }

            return Page();
        }

        public class LoginResponse
        {
            public string Token { get; set; }
        }
    }
}
