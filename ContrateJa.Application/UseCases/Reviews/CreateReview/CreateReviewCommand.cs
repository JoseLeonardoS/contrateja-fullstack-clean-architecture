namespace ContrateJa.Application.UseCases.Reviews.CreateReview;

public sealed class CreateReviewCommand
{
  public long ReviewerId { get; }
  public long ReviewedId { get; }
  public long JobId { get; }
  public int Rating { get; }
  public string Comment { get; }


  public CreateReviewCommand(long reviewerId, long reviewedId, long jobId, int rating, string comment)
  {
    ReviewerId = reviewerId;
    ReviewedId = reviewedId;
    JobId = jobId;
    Rating = rating;
    Comment = comment;
  }
}