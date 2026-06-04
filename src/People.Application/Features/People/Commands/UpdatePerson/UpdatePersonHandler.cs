using AutoMapper;
using MediatR;
using People.Application.Common.Dtos;
using People.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace People.Application.Features.People.Commands.UpdatePerson;

public class UpdatePersonHandler : IRequestHandler<UpdatePersonCommand, PersonDetailDto?>
{
    private readonly IPersonRepository _repository;
    private readonly IMapper _mapper;

    public UpdatePersonHandler(IPersonRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PersonDetailDto?> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (person == null)
            return null;

        person.Name = request.Name;
        person.Role = request.Role;
        person.Department = request.Department;
        person.Email = request.Email;
        if (!string.IsNullOrWhiteSpace(request.Status))
        {
            person.Status = request.Status;
        }

        var updated = await _repository.UpdateAsync(person, cancellationToken);
        return _mapper.Map<PersonDetailDto>(updated);
    }
}
