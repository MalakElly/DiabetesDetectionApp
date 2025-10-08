using RiskService.API.Models;
using System.Globalization;
using System.Text;

namespace RiskService.API.Services
{
    public class RiskService
    {
        public string CalculerRisque(PatientDto patient, List<NoteDto> notes)
        {
            if (patient == null || notes == null || !notes.Any())
                return "unknown";


            var content = RemoveAccents(string.Join(" ", notes.Select(n => n.Contenu))).ToLowerInvariant();


            string[] triggers = {
                "hemoglobine a1c", "microalbumine", "taille", "poids",
                "fumeur", "fumeuse", "anormal", "cholesterol",
                "vertige", "vertiges", "rechute", "reaction", "anticorps","eleve"
            };


            int triggerCount = triggers.Count(trigger => content.Contains(trigger));

            // Calcule age 
            var birthDate = patient.DateOfBirth.ToDateTime(TimeOnly.MinValue);
            int age = DateTime.Now.Year - birthDate.Year;
            if (DateTime.Now < birthDate.AddYears(age))
                age--;

            bool isMale = patient.Gender == GenderType.Homme;
            bool isFemale = patient.Gender == GenderType.Femme;

          
            if (triggerCount == 0)
                return "None";

            // Risque limité 
            if (triggerCount >= 2 && triggerCount <= 5 && age > 30)
                return "Borderline";
            if (age < 30)
            {
                if (isMale && triggerCount >= 5)
                    return "EarlyOnset";
                if (isFemale && triggerCount >= 7)
                    return "EarlyOnset";
            }
            else if (age > 30 && triggerCount >= 8)
            {
                return "EarlyOnset";
            }
            // Danger
            if (age < 30)
            {
                if (isMale && triggerCount >= 3)
                    return "InDanger";
                if (isFemale && triggerCount >= 4)
                    return "InDanger";
            }
            else
            {
                if (triggerCount == 6 || triggerCount == 7)
                    return "InDanger";
            }

 
        

            //par défaut
            return "None";
        }

        // Retirer les accents des notes
        private static string RemoveAccents(string text)
        {
            var normalized = text.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (var c in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }
            return sb.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
