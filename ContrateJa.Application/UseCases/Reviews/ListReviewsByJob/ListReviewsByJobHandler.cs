using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.UseCases.Shared;
using MediatR;

namespace ContrateJa.Application.UseCases.Reviews.ListReviewsByJob;

public sealed class ListReviewsByJobHandler : IRequestHandler<ListReviewsByJobQuery, IReadOnlyList<ReviewResponse>>
{
  private readonly IReviewRepository _reviewRepository;

  public ListReviewsByJobHandler(IReviewRepository reviewRepository)
    => _reviewRepository = reviewRepository;

  public async Task<IReadOnlyList<ReviewResponse>> Handle(ListReviewsByJobQuery query, CancellationToken ct = default)
  {
    var list = await _reviewRepository.ListByJobId(query.JobId, ct);
    
    return list.Select(review => new ReviewResponse(
        review.Id,
        review.ReviewerId,
        review.ReviewedId,
        review.JobId,
        review.Rating,
        review.Comment,
        review.CreatedAt,
        review.UpdatedAt,
        review.SubmittedAt))
      .ToList();
  }
}