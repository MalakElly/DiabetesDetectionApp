
using FrontEnd.Services;
using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages.Patients
{
    public class IndexModel : PageModel
    {
        private readonly PatientService _service;

        public IndexModel(PatientService service)
        {
            _service = service;
        }

        public List<PatientDto> Patients { get; set; }

        public async Task OnGetAsync()
        {
            Patients = await _service.GetPatientsAsync();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _service.DeletePatientAsync(id);
            return RedirectToPage();
        }
    }
}
