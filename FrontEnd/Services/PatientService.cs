using PatientService.Core.Entities;
using System.Net.Http.Json;

namespace FrontEnd.Services
{
    public class PatientService
    {
        private readonly HttpClient _httpClient;

        public PatientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

      
        public async Task<List<Patient>> GetPatientsAsync()
        {
            var patients = await _httpClient.GetFromJsonAsync<List<Patient>>("patients");
            return patients ?? new List<Patient>();
        }

    
        public async Task<Patient?> GetPatientByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"patients/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Patient>();
            }

            Console.WriteLine($"Erreur {response.StatusCode} lors de la récupération du patient {id}");
            return null;
        }

        public async Task AddPatientAsync(Patient patient)
        {
            var response = await _httpClient.PostAsJsonAsync("patients", patient);

            if (!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erreur lors de l’ajout du patient : {response.StatusCode} - {message}");
            }
        }

  
        public async Task UpdatePatientAsync(Patient patient)
        {
            var response = await _httpClient.PutAsJsonAsync($"patients/{patient.Id}", patient);

            if (!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erreur lors de la mise à jour du patient : {response.StatusCode} - {message}");
            }
        }

     
        public async Task DeletePatientAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"patients/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erreur lors de la suppression du patient : {response.StatusCode} - {message}");
            }
        }
    }
}
