using MediatR;
using People.Application.Common.Dtos;

namespace People.Application.Features.People.Commands.UpdatePerson;

public record UpdatePersonCommand(
    int Id,
    string Name,
    string Role,
    string Department,
    string Email,
    string Status) : IRequest<PersonDetailDto?>;
