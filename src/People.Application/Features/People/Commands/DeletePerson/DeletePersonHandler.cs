using MediatR;
using People.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace People.Application.Features.People.Commands.DeletePerson;

public class DeletePersonHandler : IRequestHandler<DeletePersonCommand>
{
    private readonly IPersonRepository _repository;

    public DeletePersonHandler(IPersonRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id, cancellationToken);
    }
}
