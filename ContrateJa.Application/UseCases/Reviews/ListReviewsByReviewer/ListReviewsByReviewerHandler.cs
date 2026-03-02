using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.DTOs;

namespace ContrateJa.Application.UseCases.Reviews.ListReviewsByReviewerUser;

public sealed class ListReviewsByReviewerUserHandler
{
  private readonly IReviewRepository _reviewRepository;

  public ListReviewsByReviewerUserHandler(IReviewRepository reviewRepository)
    => _reviewRepository = reviewRepository;

  public async Task<IReadOnlyList<ReviewDto>> Execute(ListReviewsByReviewerUserQuery query, CancellationToken ct = default)
  {
    if (query is null)
      throw new ArgumentNullException(nameof(query));

    if (query.ReviewerId <= 0)
      throw new ArgumentOutOfRangeException(nameof(query.ReviewerId));

    var list = await _reviewRepository.ListByReviewerId(query.ReviewerId, ct);

    return list.Select(r => new ReviewDto(
      r.Id,
      r.ReviewerId,
      r.ReviewedId,
      r.JobId,
      r.Rating,
      r.Comment,
      r.SubmittedAt,
      r.UpdatedAt))
      .ToList();
  }
}