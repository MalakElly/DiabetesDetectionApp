using FrontEnd.Services;
using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages.Patients
{
    public class EditModel : PageModel
    {
        private readonly PatientService _service;

        public EditModel(PatientService service)
        {
            _service = service;
        }

        [BindProperty]
        public PatientDto Patient { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var patients = await _service.GetPatientsAsync();
            Patient = patients.FirstOrDefault(p => p.Id == id);

            if (Patient == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _service.UpdatePatientAsync(Patient);
            return RedirectToPage("Index");
        }
    }
}
