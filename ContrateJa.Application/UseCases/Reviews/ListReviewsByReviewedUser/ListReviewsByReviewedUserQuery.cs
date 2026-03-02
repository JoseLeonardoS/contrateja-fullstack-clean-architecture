namespace ContrateJa.Application.UseCases.Reviews.ListReviewsByReviewedUser;

public sealed class ListReviewsByReviewedUserQuery
{
  public long ReviewedId { get; }

  public ListReviewsByReviewedUserQuery(long reviewedId)
    => ReviewedId = reviewedId;
}