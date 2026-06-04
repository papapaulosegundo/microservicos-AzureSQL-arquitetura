using Microsoft.EntityFrameworkCore;
using People.Domain.Entities;
using People.Domain.Interfaces;
using People.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace People.Infrastructure.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly PeopleDbContext _context;

    public PersonRepository(PeopleDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<Person>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await _context.People.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Person?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.People.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<Person> CreateAsync(Person person, CancellationToken cancellationToken = default)
    {
        person.CreatedAtUtc = DateTimeOffset.UtcNow;
        person.LastUpdatedAtUtc = DateTimeOffset.UtcNow;
        await _context.People.AddAsync(person, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return person;
    }

    public async Task<Person> UpdateAsync(Person person, CancellationToken cancellationToken = default)
    {
        person.LastUpdatedAtUtc = DateTimeOffset.UtcNow;
        _context.People.Update(person);
        await _context.SaveChangesAsync(cancellationToken);
        return person;
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var person = await GetByIdAsync(id, cancellationToken);
        if (person != null)
        {
            _context.People.Remove(person);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
