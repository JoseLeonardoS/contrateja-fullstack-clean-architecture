using ContrateJa.Application.UseCases.FreelancerAreas.CreateFreelancerArea;
using ContrateJa.Application.UseCases.FreelancerAreas.GetFreelancerAreaById;
using ContrateJa.Application.UseCases.FreelancerAreas.ListFreelancerAreas;
using ContrateJa.Application.UseCases.FreelancerAreas.RemoveFreelancerArea;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContrateJa.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class FreelancerAreasController : ControllerBase
{
    private readonly IMediator _mediator;

    public FreelancerAreasController(IMediator mediator)
        => _mediator = mediator;

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id, CancellationToken ct = default)
        => Ok(await _mediator.Send(new GetFreelancerAreaByIdQuery(id), ct));

    [HttpGet("freelancer/{freelancerId:long}")]
    public async Task<IActionResult> ListByFreelancer(long freelancerId, CancellationToken ct = default)
        => Ok(await _mediator.Send(new ListFreelancerAreasQuery(freelancerId), ct));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateFreelancerAreaCommand command, CancellationToken ct = default)
    {
        await _mediator.Send(command, ct);
        return Created();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id, CancellationToken ct = default)
    {
        await _mediator.Send(new RemoveFreelancerAreaCommand(id), ct);
        return NoContent();
    }
}