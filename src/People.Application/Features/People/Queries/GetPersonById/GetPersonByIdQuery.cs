using MediatR;
using People.Application.Common.Dtos;

namespace People.Application.Features.People.Queries.GetPersonById;

public record GetPersonByIdQuery(int Id) : IRequest<PersonDetailDto?>;
