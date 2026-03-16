using ContrateJa.Application.UseCases.Jobs.CloseJob;
using ContrateJa.Application.UseCases.Jobs.CreateJob;
using ContrateJa.Application.UseCases.Jobs.DeleteJob;
using ContrateJa.Application.UseCases.Jobs.GetJobById;
using ContrateJa.Application.UseCases.Jobs.ListJobs;
using ContrateJa.Application.UseCases.Jobs.ListJobsByContractor;
using ContrateJa.Application.UseCases.Jobs.UpdateJobAddress;
using ContrateJa.Application.UseCases.Jobs.UpdateJobDescription;
using ContrateJa.Application.UseCases.Jobs.UpdateJobStatus;
using ContrateJa.Application.UseCases.Jobs.UpdateJobTitle;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContrateJa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class JobsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JobsController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> ListJobs(CancellationToken ct = default)
            => Ok(await _mediator.Send(new ListJobsQuery(), ct));

        [HttpGet("{jobId:long}")]
        public async Task<IActionResult> GetJobById(long jobId, CancellationToken ct = default)
            => Ok(await _mediator.Send(new GetJobByIdQuery(jobId), ct));
        
        [HttpGet("contractor/{contractorId:long}")]
        public async Task<IActionResult> GetJobByContractorId(long contractorId, CancellationToken ct = default)
            => Ok(await _mediator.Send(new ListJobsByContractorQuery(contractorId), ct));

        [HttpPost("create")]
        public async Task<IActionResult> CreateJob([FromBody] CreateJobCommand createJob, CancellationToken ct = default)
        {
            await _mediator.Send(createJob, ct);
            return Created();
        }

        [HttpPut("update-title")]
        public async Task<IActionResult> UpdateTitle([FromBody] UpdateJobTitleCommand updateJobTitle,
            CancellationToken ct = default)
        {
            await _mediator.Send(updateJobTitle, ct);
            return NoContent();
        }
        
        [HttpPut("update-description")]
        public async Task<IActionResult> UpdateDescription([FromBody] UpdateJobDescriptionCommand updateJobDescription,
            CancellationToken ct = default)
        {
            await _mediator.Send(updateJobDescription, ct);
            return NoContent();
        }

        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateJobStatusCommand updateJobStatus,
            CancellationToken ct = default)
        {
            await _mediator.Send(updateJobStatus, ct);
            return NoContent();
        }

        [HttpPut("update-address")]
        public async Task<IActionResult> UpdateJobAddress([FromBody] UpdateAddressCommand updateJobAddress,
            CancellationToken ct = default)
        {
            await _mediator.Send(updateJobAddress, ct);
            return NoContent();
        }

        [HttpPatch("close")]
        public async Task<IActionResult> CloseJob([FromBody] CloseJobCommand closeJob, CancellationToken ct = default)
        {
            await _mediator.Send(closeJob, ct);
            return NoContent();
        }

        [HttpDelete("{jobId:long}")]
        public async Task<IActionResult> DeleteJob(long jobId, CancellationToken ct = default)
        {
            await  _mediator.Send(new DeleteJobCommand(jobId), ct);
            return NoContent();
        }
    }
}
