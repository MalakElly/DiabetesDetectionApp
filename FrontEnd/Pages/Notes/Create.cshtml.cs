using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace FrontEnd.Pages.Notes
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public CreateModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [BindProperty]
        public string Content { get; set; }

        [BindProperty(SupportsGet = true)]
        public string PatientId { get; set; }

        public void OnGet(string patientId)
        {
            PatientId = patientId;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Content))
            {
                ModelState.AddModelError(string.Empty, "La note ne peut pas être vide.");
                return Page();
            }

            var newNote = new
            {
                PatientId = PatientId,
                Content = Content,
                CreatedAt = DateTime.UtcNow
            };

            var response = await _httpClient.PostAsJsonAsync("notes", newNote);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Notes/Index", new { patientId = PatientId });
            }

            ModelState.AddModelError(string.Empty, "Erreur lors de l’ajout de la note.");
            return Page();
        }
    }
}
