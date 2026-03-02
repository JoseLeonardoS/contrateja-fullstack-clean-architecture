namespace ContrateJa.Application.UseCases.Reviews.ListReviewsByReviewerUser;

public sealed class ListReviewsByReviewerUserQuery
{
  public long ReviewerId { get; }

  public ListReviewsByReviewerUserQuery(long reviewerId)
    => ReviewerId = reviewerId;
}