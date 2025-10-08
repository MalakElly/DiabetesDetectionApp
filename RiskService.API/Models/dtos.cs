namespace RiskService.API.Models
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public GenderType Gender { get; set; }
    }

    public enum GenderType
    {
        Homme = 1,
        Femme = 2,
        NonSpecifie =0
    }

    public class NoteDto
    {
        public string Id { get; set; }
        public string PatientId { get; set; }
        public string Contenu { get; set; }
        public DateTime Date { get; set; }
    }
}
