using FrontEnd.Models;
using FrontEnd.Services;
using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Frontend.Pages.Patients
{
    public class CreateModel : PageModel
    {
        private readonly PatientService _service;

        public CreateModel(PatientService service)
        {
            _service = service;
        }

        [BindProperty]
        public PatientDto Patient { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _service.AddPatientAsync(Patient);
            return RedirectToPage("Index");
        }
    }
}
