using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NotesService.API.Models
{
    public class Note
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } 

        public string PatientId { get; set; } = null!; 
        public string MedecinId { get; set; } = null!;
        public string Contenu { get; set; } = null!;
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
