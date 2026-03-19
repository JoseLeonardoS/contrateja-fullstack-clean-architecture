using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.UseCases.Shared;
using MediatR;

namespace ContrateJa.Application.UseCases.Reviews.ListReviewsByReviewedUser;

public sealed class ListReviewsByReviewedUserHandler : IRequestHandler<ListReviewsByReviewedUserQuery, IReadOnlyList<ReviewResponse>>
{
  private readonly IReviewRepository _reviewRepository;

  public ListReviewsByReviewedUserHandler(IReviewRepository reviewRepository)
    => _reviewRepository = reviewRepository;

  public async Task<IReadOnlyList<ReviewResponse>> Handle(ListReviewsByReviewedUserQuery query, CancellationToken ct = default)
  {
    var list = await _reviewRepository.ListByReviewedId(query.ReviewedId, ct);
    
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