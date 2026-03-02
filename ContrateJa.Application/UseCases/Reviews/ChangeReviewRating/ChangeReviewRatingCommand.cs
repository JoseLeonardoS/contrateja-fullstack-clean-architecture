namespace ContrateJa.Application.UseCases.Reviews.ChangeReviewRating;

public sealed class ChangeReviewRatingCommand
{
  public long ReviewId { get; }
  public int NewRating { get; }

  public ChangeReviewRatingCommand(long reviewId, int newRating)
  {
    ReviewId = reviewId;
    NewRating = newRating;
  }
}