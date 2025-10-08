using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PatientService.Core.Entities;


namespace FrontEnd.Pages.Patients
{
    public class DeleteModel : PageModel
    {
            private readonly FrontEnd.Services.PatientService _service;

            public DeleteModel(FrontEnd.Services.PatientService service)
            {
                _service = service;
            }

            [BindProperty]
            public Patient Patient { get; set; }

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
          
            
           await _service.DeletePatientAsync(Patient.Id);
           return RedirectToPage("Index");
        }
        
    }

}