using ContrateJa.Application.UseCases.CompletedJobs.CompleteJob;
using ContrateJa.Application.UseCases.CompletedJobs.GetCompletedJobById;
using ContrateJa.Application.UseCases.CompletedJobs.ListCompletedJobs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContrateJa.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class CompletedJobsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CompletedJobsController(IMediator mediator)
        => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> ListAll(CancellationToken ct = default)
        => Ok(await _mediator.Send(new ListCompletedJobsQuery(), ct));

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id, CancellationToken ct = default)
        => Ok(await _mediator.Send(new GetCompletedJobByIdQuery(id), ct));

    [HttpPost]
    public async Task<IActionResult> Complete([FromBody] CompleteJobCommand command, CancellationToken ct = default)
    {
        await _mediator.Send(command, ct);
        return Created();
    }
}