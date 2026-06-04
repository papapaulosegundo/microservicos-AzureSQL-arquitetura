using People.Domain.Entities;
using System;
using System.Linq;

namespace People.Infrastructure.Persistence;

public static class DbInitializer
{
    public static void Initialize(PeopleDbContext context)
    {
        // Check if database contains any people
        if (context.People.Any())
        {
            return; // DB has been seeded
        }

        var initialPeople = new Person[]
        {
            new()
            {
                Name = "Ana Souza",
                Role = "Analista RH",
                Department = "People",
                Email = "ana.souza@bff.local",
                Status = "active",
                Summary = "Responsável pela jornada de admissão e indicadores de onboarding.",
                CreatedAtUtc = DateTimeOffset.UtcNow.AddDays(-10),
                LastUpdatedAtUtc = DateTimeOffset.UtcNow.AddDays(-10)
            },
            new()
            {
                Name = "Carlos Lima",
                Role = "Tech Recruiter",
                Department = "People",
                Email = "carlos.lima@bff.local",
                Status = "active",
                Summary = "Acompanha trilhas de capacitação e performance dos líderes.",
                CreatedAtUtc = DateTimeOffset.UtcNow.AddDays(-5),
                LastUpdatedAtUtc = DateTimeOffset.UtcNow.AddDays(-5)
            },
            new()
            {
                Name = "Marina Costa",
                Role = "BP RH",
                Department = "Operations",
                Email = "marina.costa@bff.local",
                Status = "inactive",
                Summary = "Organiza fluxos de documentos contratuais e assinaturas digitais.",
                CreatedAtUtc = DateTimeOffset.UtcNow.AddDays(-2),
                LastUpdatedAtUtc = DateTimeOffset.UtcNow.AddDays(-2)
            }
        };

        context.People.AddRange(initialPeople);
        context.SaveChanges();
    }
}
