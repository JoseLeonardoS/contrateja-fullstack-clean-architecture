namespace ContrateJa.Application.UseCases.Reviews.EditReviewComment;

public sealed class EditReviewCommentCommand
{
  public long ReviewId { get; }
  public string NewComment { get; }

  public EditReviewCommentCommand(long reviewId, string newComment)
  {
    ReviewId = reviewId;
    NewComment = newComment;
  }
}