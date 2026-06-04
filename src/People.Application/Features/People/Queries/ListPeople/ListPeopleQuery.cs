using MediatR;
using People.Application.Common.Dtos;
using System.Collections.Generic;

namespace People.Application.Features.People.Queries.ListPeople;

public record ListPeopleQuery : IRequest<IReadOnlyCollection<PersonSummaryDto>>;
