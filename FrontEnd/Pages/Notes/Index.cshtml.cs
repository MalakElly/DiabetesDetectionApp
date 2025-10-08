using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace FrontEnd.Pages.Notes
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public IndexModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<NoteViewModel> Notes { get; set; } = new();
        [BindProperty(SupportsGet = true)]
        public string PatientId { get; set; }

        [BindProperty]
        public string NewNoteContent { get; set; }

        [BindProperty]
        public string UpdatedContent { get; set; }

        public string NoteBeingEdited { get; set; }

        public async Task OnGetAsync(string patientId)
        {
            PatientId = patientId;
            await LoadNotesAsync();
        }

        private async Task LoadNotesAsync()
        {
            var response = await _httpClient.GetAsync($"notes/patient/{PatientId}");
            if (response.IsSuccessStatusCode)
            {
                var notes = await response.Content.ReadFromJsonAsync<List<NoteViewModel>>();
                Notes = notes ?? new List<NoteViewModel>();
            }
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (string.IsNullOrWhiteSpace(NewNoteContent))
            {
                ModelState.AddModelError(string.Empty, "La note ne peut pas être vide.");
                await LoadNotesAsync();
                return Page();
            }

            var newNote = new
            {
                PatientId = PatientId,
                MedecinId = "1",
                Contenu = NewNoteContent,
                Date = DateTime.UtcNow
            };

            var response = await _httpClient.PostAsJsonAsync("notes", newNote);
            if (response.IsSuccessStatusCode)
                return RedirectToPage(new { patientId = PatientId });

            ModelState.AddModelError(string.Empty, "Erreur lors de l’ajout de la note.");
            await LoadNotesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"notes/{id}");
            return RedirectToPage(new { patientId = PatientId });
        }

        public async Task<IActionResult> OnPostEditAsync(string id)
        {
            NoteBeingEdited = id;
            await LoadNotesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateAsync(string id)
        {
            var updateNote = new { Contenu = UpdatedContent };
            var response = await _httpClient.PutAsJsonAsync($"notes/{id}", updateNote);
            return RedirectToPage(new { patientId = PatientId });
        }

        public IActionResult OnPostCancelEdit()
        {
            return RedirectToPage(new { patientId = PatientId });
        }
    }

    public class NoteViewModel
    {
        public string Id { get; set; }
        public string PatientId { get; set; }
        public string Contenu { get; set; }
        public DateTime Date { get; set; }
    }
}
