using MediatR;

namespace People.Application.Features.People.Commands.DeletePerson;

public record DeletePersonCommand(int Id) : IRequest;
