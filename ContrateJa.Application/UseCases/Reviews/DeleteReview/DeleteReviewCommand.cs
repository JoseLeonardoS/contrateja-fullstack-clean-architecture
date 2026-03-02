namespace ContrateJa.Application.UseCases.Reviews.DeleteReview;

public sealed class DeleteReviewCommand
{
  public long ReviewId { get; }

  public DeleteReviewCommand(long reviewId)
    => ReviewId = reviewId;
}