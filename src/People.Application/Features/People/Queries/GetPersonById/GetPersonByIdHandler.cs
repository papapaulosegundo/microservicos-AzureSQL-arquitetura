using AutoMapper;
using MediatR;
using People.Application.Common.Dtos;
using People.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace People.Application.Features.People.Queries.GetPersonById;

public class GetPersonByIdHandler : IRequestHandler<GetPersonByIdQuery, PersonDetailDto?>
{
    private readonly IPersonRepository _repository;
    private readonly IMapper _mapper;

    public GetPersonByIdHandler(IPersonRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PersonDetailDto?> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
    {
        var person = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (person == null)
            return null;

        return _mapper.Map<PersonDetailDto>(person);
    }
}
