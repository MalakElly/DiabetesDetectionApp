using fs=FrontEnd.Services;
using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;

namespace FrontEnd.Pages.Patients
{
    public class IndexModel : PageModel
    {
        private readonly fs.PatientService _service;
        private readonly HttpClient _httpClient;

        public IndexModel(fs.PatientService service, IHttpClientFactory httpClientFactory)
        {
            _service = service;
            _httpClient = httpClientFactory.CreateClient("GatewayApi"); //client
        }


        public List<PatientViewModel> Patients { get; set; } = new();

        public async Task OnGetAsync()
        {
            // Récupérer les patients via service local
            var backendPatients = await _service.GetPatientsAsync();

            // Mapper vers modèle front
            Patients = backendPatients.Select(p => new PatientViewModel
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                DateOfBirth = p.DateOfBirth,
                Gender = p.Gender.ToString(),
                Address = p.Address,
                PhoneNumber = p.PhoneNumber
            }).ToList();

            //appeler le RiskService via le Gateway
            foreach (var patient in Patients)
            {
                try
                {
                    Console.WriteLine($"[Front] Appel RiskService pour {patient.Id}");
                    var risk = await _httpClient.GetFromJsonAsync<RiskResult>($"risk/{patient.Id}");
                    patient.Risque = risk?.Risque ?? "Inconnu";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Front] Erreur appel RiskService : {ex.Message}");
                    patient.Risque = "Inconnu";
                }
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _service.DeletePatientAsync(id);
            return RedirectToPage();
        }
    }

    public class RiskResult
    {
        public string PatientId { get; set; } = string.Empty;
        public string Risque { get; set; } = string.Empty;
    }
}
