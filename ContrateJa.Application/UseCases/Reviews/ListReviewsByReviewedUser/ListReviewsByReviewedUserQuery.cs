using ContrateJa.Application.UseCases.Shared;
using MediatR;

namespace ContrateJa.Application.UseCases.Reviews.ListReviewsByReviewedUser;

public sealed class ListReviewsByReviewedUserQuery : IRequest<IReadOnlyList<ReviewResponse>>
{
  public long ReviewedId { get; }

  public ListReviewsByReviewedUserQuery(long reviewedId)
    => ReviewedId = reviewedId > 0 ? reviewedId
      : throw new ArgumentOutOfRangeException(nameof(reviewedId));
}