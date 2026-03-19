using MediatR;

namespace ContrateJa.Application.UseCases.Reviews.EditReview;

public sealed class EditReviewCommand : IRequest
{
  public long ReviewId { get; }
  public int Rating { get; }
  public string Comment { get; }

  public EditReviewCommand(long reviewId, int rating, string comment)
  {
    ReviewId = reviewId > 0 ? reviewId : throw new ArgumentOutOfRangeException(nameof(reviewId));
    Rating = rating;
    Comment = comment;
  }
}