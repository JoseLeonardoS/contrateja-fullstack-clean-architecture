namespace ContrateJa.Application.UseCases.Reviews.GetReviewById;

public sealed class GetReviewByIdQuery
{
  public long Id { get; }

  public GetReviewByIdQuery(long id)
    => Id = id;
}