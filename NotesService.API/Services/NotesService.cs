using MongoDB.Driver;
using NotesService.API.Config;
using NotesService.API.Models;
using Microsoft.Extensions.Options;
using MongoDatabaseSettings = NotesService.API.Config.MongoDatabaseSettings;

namespace NotesService.API.Services
{
    public class NotesService
    {
        private readonly IMongoCollection<Note> _notes;

        public NotesService(IOptions<MongoDatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _notes = database.GetCollection<Note>(settings.Value.NotesCollectionName);
        }

        public List<Note> GetNotesByPatient(string patientId) =>
            _notes.Find(n => n.PatientId == patientId).ToList();

        public Note GetNoteById(string id) =>
            _notes.Find(n => n.Id == id).FirstOrDefault();

        //public Note Create(Note note)
        //{
         
        //    note.Id = null;

        //    //Si MedecinId non fourni
        //    if (string.IsNullOrWhiteSpace(note.MedecinId))
        //        note.MedecinId = "1";

        //    _notes.InsertOne(note);
        //    return note;
        //}
        public Note Create(Note note)
        {
            if (string.IsNullOrWhiteSpace(note.PatientId))
                throw new ArgumentException("PatientId requis", nameof(note.PatientId));

            if (string.IsNullOrWhiteSpace(note.Contenu))
                throw new ArgumentException("Contenu requis", nameof(note.Contenu));

            note.Id = null;
            note.Date = DateTime.UtcNow;

            if (string.IsNullOrWhiteSpace(note.MedecinId))
                note.MedecinId = "1";

            _notes.InsertOne(note);
            return note;
        }
        public void Update(string id, Note noteIn) =>
          _notes.ReplaceOne(note => note.Id == id, noteIn);

        public void Delete(string id) =>
            _notes.DeleteOne(note => note.Id == id);


    }
}
