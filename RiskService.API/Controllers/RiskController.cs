using Microsoft.AspNetCore.Mvc;
using RiskService.API.Models;
using rs=RiskService.API.Services;
using System.Net.Http.Json;

namespace RiskService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RiskController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly rs.RiskService _riskService;

   

        public RiskController(IHttpClientFactory httpClientFactory, rs.RiskService riskService)
        {
            _httpClient = httpClientFactory.CreateClient("DefaultClient");
            _riskService = riskService;
        }


        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetRisk(string patientId)
        {
            try
            {
                // Appels vers les microservices
                var patient = await _httpClient.GetFromJsonAsync<PatientDto>($"patients/{patientId}");

                if (patient == null)
                    return NotFound($"Patient {patientId} introuvable.");

            
                var notes = await _httpClient.GetFromJsonAsync<List<NoteDto>>($"notes/patient/{patientId}");


                // Calcul du risque à partir des données récupérées
                var niveauRisque = _riskService.CalculerRisque(patient, notes);

                var result = new RiskResult
                {
                    PatientId = patientId,
                    Risque = niveauRisque
                };
                Console.WriteLine($"[RiskService] Patient {patient.FirstName} {patient.LastName}, Notes: {notes.Count}, Risque: {niveauRisque}");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur serveur : {ex.Message}");
            }
        }
    }
}
