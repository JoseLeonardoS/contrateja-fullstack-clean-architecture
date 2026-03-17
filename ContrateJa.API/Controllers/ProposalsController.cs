using ContrateJa.Application.UseCases.Proposals.AcceptProposal;
using ContrateJa.Application.UseCases.Proposals.CreateProposal;
using ContrateJa.Application.UseCases.Proposals.DeleteProposal;
using ContrateJa.Application.UseCases.Proposals.EditProposal;
using ContrateJa.Application.UseCases.Proposals.EditProposalAmount;
using ContrateJa.Application.UseCases.Proposals.EditProposalCoverLetter;
using ContrateJa.Application.UseCases.Proposals.GetProposalById;
using ContrateJa.Application.UseCases.Proposals.ListProposalsByFreelancer;
using ContrateJa.Application.UseCases.Proposals.ListProposalsByJob;
using ContrateJa.Application.UseCases.Proposals.RejectProposal;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContrateJa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class ProposalsController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public ProposalsController(IMediator mediator) 
            =>  _mediator = mediator;

        [HttpGet("{proposalId:long}")]
        public async Task<IActionResult> GetById(long proposalId, CancellationToken ct = default)
            => Ok(await _mediator.Send(new GetProposalByIdQuery(proposalId), ct));

        [HttpGet("freelancer/{freelancerId:long}")]
        public async Task<IActionResult> GetByFreelancerId(long freelancerId, CancellationToken ct = default)
            => Ok(await _mediator.Send(new ListProposalsByFreelancerQuery(freelancerId), ct));
        
        [HttpGet("job/{jobId:long}")]
        public async Task<IActionResult> GetByJobId(long jobId, CancellationToken ct = default)
            => Ok(await _mediator.Send(new ListProposalsByJobQuery(jobId), ct));

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateProposalCommand createProposal,
            CancellationToken ct = default)
        {
            await _mediator.Send(createProposal, ct);
            return Created();
        }

        [HttpPatch("accept")]
        public async Task<IActionResult> Accept([FromBody] AcceptProposalCommand acceptProposal,
            CancellationToken ct = default)
        {
            await _mediator.Send(acceptProposal, ct);
            return Accepted();
        }
        
        [HttpPatch("reject")]
        public async Task<IActionResult> Reject([FromBody] RejectProposalCommand rejectProposal,
            CancellationToken ct = default)
        {
            await _mediator.Send(rejectProposal, ct);
            return NoContent();
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] EditProposalCommand editProposal,
            CancellationToken ct = default)
        {
            await _mediator.Send(editProposal, ct);
            return NoContent();
        }
        
        [HttpPut("edit-amount")]
        public async Task<IActionResult> EditAmount([FromBody] EditProposalAmountCommand editAmount,
            CancellationToken ct = default)
        {
            await _mediator.Send(editAmount, ct);
            return NoContent();
        }
        
        [HttpPut("edit-coverLetter")]
        public async Task<IActionResult> EditCoverLetter([FromBody] EditProposalCoverLetterCommand editCoverLetter,
            CancellationToken ct = default)
        {
            await _mediator.Send(editCoverLetter, ct);
            return NoContent();
        }

        [HttpDelete("{proposalId:long}")]
        public async Task<IActionResult> Delete(long proposalId, CancellationToken ct = default)
        {
            await _mediator.Send(new DeleteProposalCommand(proposalId), ct);
            return NoContent();
        }
    }
}
