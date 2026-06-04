using People.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace People.Domain.Interfaces;

public interface IPersonRepository
{
    Task<IReadOnlyCollection<Person>> ListAsync(CancellationToken cancellationToken = default);
    Task<Person?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Person> CreateAsync(Person person, CancellationToken cancellationToken = default);
    Task<Person> UpdateAsync(Person person, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
