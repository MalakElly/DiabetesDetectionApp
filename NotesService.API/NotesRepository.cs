using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NotesService.API.Models;

public class NotesRepository
{
    private readonly IMongoCollection<Note> _notes;

    public NotesRepository(IOptions<MongoDatabaseSettings> settings, IMongoClient client)
    {
        //var database = client.GetDatabase(settings.Value.DatabaseName);
        //_notes = database.GetCollection<Note>(settings.Value.NotesCollectionName);
    }

    public async Task<List<Note>> GetNotesByPatientAsync(string patientId) =>
        await _notes.Find(n => n.PatientId == patientId).ToListAsync();

    public async Task AddNoteAsync(Note note) =>
        await _notes.InsertOneAsync(note);
}
