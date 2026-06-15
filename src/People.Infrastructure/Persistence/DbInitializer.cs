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
            },
            new()
            {
                Name = "Juliana Reis",
                Role = "Gerente de DHO",
                Department = "People",
                Email = "juliana.reis@gestaorh.local",
                Status = "active",
                Summary = "Liderança na área de Desenvolvimento Humano e Organizacional e engajamento interno.",
                CreatedAtUtc = DateTimeOffset.UtcNow.AddDays(-30),
                LastUpdatedAtUtc = DateTimeOffset.UtcNow.AddDays(-15)
            },
            new()
            {
                Name = "Roberto Silveira",
                Role = "Analista de DP",
                Department = "Operations",
                Email = "roberto.silveira@gestaorh.local",
                Status = "active",
                Summary = "Responsável por processamento da folha de pagamento, benefícios e férias.",
                CreatedAtUtc = DateTimeOffset.UtcNow.AddDays(-25),
                LastUpdatedAtUtc = DateTimeOffset.UtcNow.AddDays(-25)
            },
            new()
            {
                Name = "Fernanda Oliveira",
                Role = "Analista de Atração de Talentos",
                Department = "People",
                Email = "fernanda.oliveira@gestaorh.local",
                Status = "active",
                Summary = "Recrutamento e seleção de profissionais para áreas de tecnologia e produto.",
                CreatedAtUtc = DateTimeOffset.UtcNow.AddDays(-12),
                LastUpdatedAtUtc = DateTimeOffset.UtcNow.AddDays(-1)
            },
            new()
            {
                Name = "Ricardo Santos",
                Role = "Diretor de RH",
                Department = "Executive",
                Email = "ricardo.santos@gestaorh.local",
                Status = "active",
                Summary = "Direcionamento estratégico e governança de políticas corporativas globais.",
                CreatedAtUtc = DateTimeOffset.UtcNow.AddDays(-90),
                LastUpdatedAtUtc = DateTimeOffset.UtcNow.AddDays(-90)
            }
        };

        context.People.AddRange(initialPeople);
        context.SaveChanges();
    }
}
