using ContrateJa.Application.UseCases.Shared;
using MediatR;

namespace ContrateJa.Application.UseCases.Reviews.ListReviewsByReviewer;

public sealed class ListReviewsByReviewerQuery : IRequest<IReadOnlyList<ReviewResponse>>
{
  public long ReviewerId { get; }

  public ListReviewsByReviewerQuery(long reviewerId)
    => ReviewerId = reviewerId > 0 ? reviewerId
        : throw new ArgumentOutOfRangeException(nameof(reviewerId));
}