using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.UseCases.Shared;
using MediatR;

namespace ContrateJa.Application.UseCases.Reviews.ListReviewsByReviewer;

public sealed class ListReviewsByReviewerHandler : IRequestHandler<ListReviewsByReviewerQuery, IEnumerable<ReviewResponse>>
{
  private readonly IReviewRepository _reviewRepository;

  public ListReviewsByReviewerHandler(IReviewRepository reviewRepository)
    => _reviewRepository = reviewRepository;

  public async Task<IEnumerable<ReviewResponse>> Handle(ListReviewsByReviewerQuery query, CancellationToken ct = default)
  {
    var list = await _reviewRepository.ListByReviewerId(query.ReviewerId, ct);
    
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