using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PatientService.Core.Entities; 

namespace FrontEnd.Pages.Patients
{
    public class CreateModel : PageModel
    {
        private readonly FrontEnd.Services.PatientService _service;

        public CreateModel(FrontEnd.Services.PatientService service)
        {
            _service = service;
        }

        [BindProperty]
        public Patient Patient { get; set; }

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
