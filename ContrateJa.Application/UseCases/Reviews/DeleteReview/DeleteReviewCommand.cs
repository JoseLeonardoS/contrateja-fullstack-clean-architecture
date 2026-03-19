using MediatR;

namespace ContrateJa.Application.UseCases.Reviews.DeleteReview;

public sealed class DeleteReviewCommand : IRequest
{
  public long ReviewId { get; }

  public DeleteReviewCommand(long reviewId)
    => ReviewId = reviewId > 0 ? reviewId : throw new ArgumentOutOfRangeException(nameof(reviewId));
}