using MediatR;

namespace ContrateJa.Application.UseCases.Reviews.CreateReview;

public sealed class CreateReviewCommand : IRequest
{
  public long ReviewerId { get; }
  public long ReviewedId { get; }
  public long JobId { get; }
  public int Rating { get; }
  public string Comment { get; }


  public CreateReviewCommand(long reviewerId, long reviewedId, long jobId, int rating, string comment)
  {
    ReviewerId = reviewerId > 0 ? reviewerId : throw new ArgumentOutOfRangeException(nameof(reviewerId));
    ReviewedId = reviewedId > 0 ? reviewedId : throw new ArgumentOutOfRangeException(nameof(reviewedId));
    JobId = jobId >  0 ? jobId : throw new ArgumentOutOfRangeException(nameof(jobId));
    Rating = rating;
    Comment = comment;
  }
}