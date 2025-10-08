using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace FrontEnd.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly HttpClient _http;

        public RegisterModel(IHttpClientFactory httpClientFactory)
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
            var response = await _http.PostAsJsonAsync("api/auth/register", Input);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Account/Login");
            }

            ModelState.AddModelError("", "Erreur lors de l'inscription.");
            return Page();
        }
    }
}
