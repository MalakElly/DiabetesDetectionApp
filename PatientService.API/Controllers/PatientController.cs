using Microsoft.AspNetCore.Mvc;

using PatientService.Core.Entities;
using PatientService.Core.Interfaces;

namespace PatientService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientRepository _repository;

        public PatientsController(IPatientRepository repository)
        {
            _repository = repository;
        }

        // GET: api/patients
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var patients = await _repository.GetAllAsync();
            return Ok(patients);
        }

        // GET: api/patients/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var patient = await _repository.GetByIdAsync(id);
            if (patient == null)
                return NotFound();
            return Ok(patient);
        }

        // Updated Create method to include explicit casting for GenderType
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Patient patientDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var patient = new Patient
            {
                FirstName = patientDto.FirstName,
                LastName = patientDto.LastName,
                DateOfBirth = patientDto.DateOfBirth,
                Gender = (PatientService.Core.Entities.GenderEntityType)patientDto.Gender, 
                Address = patientDto.Address,
                PhoneNumber = patientDto.PhoneNumber
            };

            await _repository.AddAsync(patient);


            return CreatedAtAction(nameof(GetById), new { id = patient.Id }, patient);
        }

        // PUT: api/patients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Patient patient)
        {
            if (id != patient.Id)
                return BadRequest("Mismatched patient ID");

            await _repository.UpdateAsync(patient);
            return NoContent();
        }

        // DELETE: api/patients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
