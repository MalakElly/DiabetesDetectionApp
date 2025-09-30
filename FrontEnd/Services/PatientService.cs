using System.Net.Http.Json;

using FrontEnd.Models;

namespace FrontEnd.Services
{
    public class PatientService
    {
        private readonly HttpClient _httpClient;

        public PatientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PatientDto>> GetPatientsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PatientDto>>("/patients");
        }

        public async Task AddPatientAsync(PatientDto patient)
        {
            await _httpClient.PostAsJsonAsync("/patients", patient);
        }

        public async Task UpdatePatientAsync(PatientDto patient)
        {
            await _httpClient.PutAsJsonAsync($"/patients/{patient.Id}", patient);
        }

        public async Task DeletePatientAsync(int id)
        {
            await _httpClient.DeleteAsync($"/patients/{id}");
        }

    }
}
