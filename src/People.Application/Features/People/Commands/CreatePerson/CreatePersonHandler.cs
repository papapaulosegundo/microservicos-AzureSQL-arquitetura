using AutoMapper;
using MediatR;
using People.Application.Common.Dtos;
using People.Domain.Entities;
using People.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace People.Application.Features.People.Commands.CreatePerson;

public class CreatePersonHandler : IRequestHandler<CreatePersonCommand, PersonDetailDto>
{
    private readonly IPersonRepository _repository;
    private readonly IMapper _mapper;

    public CreatePersonHandler(IPersonRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PersonDetailDto> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = new Person
        {
            Name = request.Name,
            Role = request.Role,
            Department = request.Department,
            Email = request.Email,
            Status = string.IsNullOrWhiteSpace(request.Status) ? "active" : request.Status,
            Summary = request.Summary ?? string.Empty
        };

        var created = await _repository.CreateAsync(person, cancellationToken);
        return _mapper.Map<PersonDetailDto>(created);
    }
}
