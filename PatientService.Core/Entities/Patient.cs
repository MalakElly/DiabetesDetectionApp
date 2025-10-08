using System;
using System.ComponentModel.DataAnnotations;

namespace PatientService.Core.Entities
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Le prénom est obligatoire.")]
        [StringLength(100, ErrorMessage = "Le prénom ne peut pas dépasser 100 caractères.")]
        [Display(Name = "Prénom")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères.")]
        [Display(Name = "Nom")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "La date de naissance est obligatoire.")]
        [Display(Name = "Date de naissance")]
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Le genre est obligatoire.")]
        [Display(Name = "Genre")]
        public GenderEntityType Gender { get; set; }

        [StringLength(250, ErrorMessage = "L’adresse ne peut pas dépasser 250 caractères.")]
        [Display(Name = "Adresse")]
        public string? Address { get; set; }

        [Phone(ErrorMessage = "Le numéro de téléphone n’est pas valide.")]
        [Display(Name = "Téléphone")]
        public string? PhoneNumber { get; set; }
    }

    public enum GenderEntityType
    {
        [Display(Name = "Non spécifié")]
        NonSpecifie = 0,

        [Display(Name = "Homme")]
        Homme = 1,

        [Display(Name = "Femme")]
        Femme = 2
    }
}
