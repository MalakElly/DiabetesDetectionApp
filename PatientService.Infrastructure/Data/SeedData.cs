using Microsoft.EntityFrameworkCore;
using PatientService.Core.Entities;

namespace PatientService.Infrastructure.Data
{

    public static class SeedData
    {
        public static void Initialize(PatientDbContext context)
        {
            context.Database.Migrate();


            if (!context.Patients.Any())
            {

                var patients = new List<Patient>
            {
                new Patient
                {
                    FirstName = "Test",
                    LastName = "TestNone",
                    DateOfBirth = new DateOnly(1966, 12, 31),
                    Gender = GenderEntityType.Femme,
                    Address = "1 Brookside St",
                    PhoneNumber = "100-222-3333"
                },
                new Patient
                {
                    FirstName = "Test",
                    LastName = "TestBorderline",
                    DateOfBirth = new DateOnly(1945, 6, 24),
                    Gender = GenderEntityType.Homme,
                    Address = "2 High St",
                    PhoneNumber = "200-333-4444"
                },
                new Patient
                {
                    FirstName = "Test",
                    LastName = "TestInDanger",
                    DateOfBirth = new DateOnly(2004, 6, 18),
                    Gender = GenderEntityType.Homme,
                    Address = "3 Club Road",
                    PhoneNumber = "300-444-5555"
                },
                new Patient
                {
                    FirstName = "Test",
                    LastName = "TestEarlyOnset",
                    DateOfBirth = new DateOnly(2002, 6, 28),
                    Gender = GenderEntityType.Femme,
                    Address = "4 Valley Dr",
                    PhoneNumber = "400-555-6666"
                }
            };

                context.Patients.AddRange(patients);
                context.SaveChanges();

                Console.WriteLine("[SeedData] Patients injectés avec succès dans la base SQL.");
            }
        }
    }

}
