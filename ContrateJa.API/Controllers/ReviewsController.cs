using ContrateJa.Application.UseCases.Reviews.ChangeReviewRating;
using ContrateJa.Application.UseCases.Reviews.CreateReview;
using ContrateJa.Application.UseCases.Reviews.DeleteReview;
using ContrateJa.Application.UseCases.Reviews.EditReview;
using ContrateJa.Application.UseCases.Reviews.EditReviewComment;
using ContrateJa.Application.UseCases.Reviews.GetAverageRatingByUser;
using ContrateJa.Application.UseCases.Reviews.GetReviewById;
using ContrateJa.Application.UseCases.Reviews.ListReviewsByJob;
using ContrateJa.Application.UseCases.Reviews.ListReviewsByReviewedUser;
using ContrateJa.Application.UseCases.Reviews.ListReviewsByReviewer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContrateJa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class ReviewsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReviewsController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet("{reviewId:long}")]
        public async Task<IActionResult> GetById(long reviewId, CancellationToken ct = default)
            => Ok(await _mediator.Send(new GetReviewByIdQuery(reviewId), ct));
        
        [HttpGet("job-id/{jobId:long}")]
        public async Task<IActionResult> ListByJobId(long jobId, CancellationToken ct = default)
            => Ok(await _mediator.Send(new ListReviewsByJobQuery(jobId), ct));
        
        [HttpGet("reviewer/{reviewerId:long}")]
        public async Task<IActionResult> ListByReviewerId(long reviewerId, CancellationToken ct = default)
            => Ok(await _mediator.Send(new ListReviewsByReviewerQuery(reviewerId), ct));
        
        [HttpGet("reviewed/{reviewedId:long}")]
        public async Task<IActionResult> ListByReviewedId(long reviewedId, CancellationToken ct = default)
            => Ok(await _mediator.Send(new ListReviewsByReviewedUserQuery(reviewedId), ct));
        
        [HttpGet("rating")]
        public async Task<IActionResult> GetAverage([FromQuery]long reviewedId, CancellationToken ct = default)
            => Ok(await _mediator.Send(new GetAverageRatingByUserQuery(reviewedId), ct));

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateReviewCommand createReview, CancellationToken ct = default)
        {
            await _mediator.Send(createReview, ct);
            return Created();
        }

        [HttpPut("update")]
        public async Task<IActionResult> EditReview([FromBody] EditReviewCommand editReview,
            CancellationToken ct = default)
        {
            await _mediator.Send(editReview, ct);
            return NoContent();
        }

        [HttpPut("edit-comment")]
        public async Task<IActionResult> EditComment([FromBody] EditReviewCommentCommand editComment,
            CancellationToken ct = default)
        {
            await _mediator.Send(editComment, ct);
            return NoContent();
        }

        [HttpPut("update-rating")]
        public async Task<IActionResult> UpdateRating([FromBody] ChangeReviewRatingCommand changeReviewRating,
            CancellationToken ct = default)
        {
            await _mediator.Send(changeReviewRating, ct);
            return NoContent();
        }
        
        [HttpDelete("{reviewId:long}")]
        public async Task<IActionResult> Delete(long reviewId, CancellationToken ct = default)
        {
            await _mediator.Send(new DeleteReviewCommand(reviewId), ct);
            return NoContent();
        }
    }
}
