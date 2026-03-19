using MediatR;

namespace ContrateJa.Application.UseCases.Reviews.GetAverageRatingByUser;

public sealed class GetAverageRatingByUserQuery : IRequest<double>
{
  public long ReviewedId { get; }

  public GetAverageRatingByUserQuery(long reviewedId)
    => ReviewedId = reviewedId > 0 ? reviewedId 
        : throw new ArgumentOutOfRangeException(nameof(reviewedId));
}