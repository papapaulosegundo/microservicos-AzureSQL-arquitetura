using MediatR;
using Microsoft.AspNetCore.Mvc;
using People.Application.Features.People.Commands.CreatePerson;
using People.Application.Features.People.Commands.DeletePerson;
using People.Application.Features.People.Commands.UpdatePerson;
using People.Application.Features.People.Queries.GetPersonById;
using People.Application.Features.People.Queries.ListPeople;
using System.Threading;
using System.Threading.Tasks;

namespace People.API.Controllers;

[ApiController]
[Route("api/people")]
public class PeopleController : ControllerBase
{
    private readonly IMediator _mediator;

    public PeopleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> List(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ListPeopleQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetPersonByIdQuery(id), cancellationToken);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePersonCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePersonCommand command, CancellationToken cancellationToken)
    {
        var updatedCommand = command with { Id = id };
        var result = await _mediator.Send(updatedCommand, cancellationToken);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeletePersonCommand(id), cancellationToken);
        return NoContent();
    }
}
