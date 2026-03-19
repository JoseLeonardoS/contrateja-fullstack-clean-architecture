using MediatR;

namespace ContrateJa.Application.UseCases.Reviews.EditReviewComment;

public sealed class EditReviewCommentCommand : IRequest
{
  public long ReviewId { get; }
  public string Comment { get; }

  public EditReviewCommentCommand(long reviewId, string comment)
  {
    ReviewId = reviewId > 0 ? reviewId : throw new ArgumentOutOfRangeException(nameof(reviewId));
    Comment = comment;
  }
}