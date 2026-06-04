using AutoMapper;
using MediatR;
using People.Application.Common.Dtos;
using People.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace People.Application.Features.People.Queries.ListPeople;

public class ListPeopleHandler : IRequestHandler<ListPeopleQuery, IReadOnlyCollection<PersonSummaryDto>>
{
    private readonly IPersonRepository _repository;
    private readonly IMapper _mapper;

    public ListPeopleHandler(IPersonRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<PersonSummaryDto>> Handle(ListPeopleQuery request, CancellationToken cancellationToken)
    {
        var people = await _repository.ListAsync(cancellationToken);
        return _mapper.Map<IReadOnlyCollection<PersonSummaryDto>>(people);
    }
}
