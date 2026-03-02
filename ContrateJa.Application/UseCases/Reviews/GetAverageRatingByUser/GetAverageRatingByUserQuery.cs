namespace ContrateJa.Application.UseCases.Reviews.GetAverageRatingByUser;

public sealed class GetAverageRatingByUserQuery
{
  public long ReviewedId { get; }

  public GetAverageRatingByUserQuery(long reviewedId)
    => ReviewedId = reviewedId;
}