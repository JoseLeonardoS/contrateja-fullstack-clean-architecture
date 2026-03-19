using MediatR;

namespace ContrateJa.Application.UseCases.Reviews.ChangeReviewRating;

public sealed class ChangeReviewRatingCommand : IRequest
{
  public long ReviewId { get; }
  public int Rating { get; }

  public ChangeReviewRatingCommand(long reviewId, int rating)
  {
    ReviewId = reviewId > 0 ? reviewId : throw new ArgumentOutOfRangeException(nameof(reviewId));
    Rating =  rating;
  }
}