using Microsoft.AspNetCore.Mvc;
using NotesService.API.Models;
using NotesService.API.Services;

namespace NotesService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly NotesService.API.Services.NotesService _notesService;

        public NotesController(NotesService.API.Services.NotesService notesService)
        {
            _notesService = notesService;
        }

        [HttpGet("patient/{patientId}")]
        public IActionResult GetNotesForPatient(string patientId)
        {
            var notes = _notesService.GetNotesByPatient(patientId);
            return Ok(notes);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var note = _notesService.GetNoteById(id);
            if (note == null) return NotFound();
            return Ok(note);
        }

        [HttpPost]
        [HttpPost]
        public IActionResult Create(Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdNote = _notesService.Create(note);
            return CreatedAtAction(nameof(GetById), new { id = createdNote.Id }, createdNote);
        }
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Note updatedNote)
        {
            var note = _notesService.GetNoteById(id);
            if (note == null)
                return NotFound();

            note.Contenu = updatedNote.Contenu;
            _notesService.Update(id, note);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var note = _notesService.GetNoteById(id);
            if (note == null)
                return NotFound();

            _notesService.Delete(id);
            return NoContent();
        }

        //public IActionResult Create(Note note)
        //{
        //    var createdNote = _notesService.Create(note);
        //    return CreatedAtAction(nameof(GetById), new { id = createdNote.Id }, createdNote);
        //}
    }
}
