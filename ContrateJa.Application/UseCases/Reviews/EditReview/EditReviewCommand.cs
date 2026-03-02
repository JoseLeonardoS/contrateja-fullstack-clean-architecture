namespace ContrateJa.Application.UseCases.Reviews.EditReview;

public sealed class EditReviewCommand
{
  public long ReviewId { get; }
  public int NewRating { get; }
  public string NewComment { get; }

  public EditReviewCommand(long reviewId, int newRating, string newComment)
  {
    ReviewId = reviewId;
    NewRating = newRating; ;
    NewComment = newComment;
  }
}