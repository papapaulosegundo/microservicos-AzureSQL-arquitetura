using MediatR;
using People.Application.Common.Dtos;

namespace People.Application.Features.People.Commands.CreatePerson;

public record CreatePersonCommand(
    string Name,
    string Role,
    string Department,
    string Email,
    string Status,
    string Summary) : IRequest<PersonDetailDto>;
