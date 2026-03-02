using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.DTOs;

namespace ContrateJa.Application.UseCases.Reviews.ListReviewsByReviewedUser;

public sealed class ListReviewsByReviewedUserHandler
{
  private readonly IReviewRepository _reviewRepository;

  public ListReviewsByReviewedUserHandler(IReviewRepository reviewRepository)
    => _reviewRepository = reviewRepository;

  public async Task<IReadOnlyList<ReviewDto>> Execute(ListReviewsByReviewedUserQuery query, CancellationToken ct = default)
  {
    if (query is null)
      throw new ArgumentNullException(nameof(query));

    if (query.ReviewedId <= 0)
      throw new ArgumentOutOfRangeException(nameof(query.ReviewedId));

    var list = await _reviewRepository.ListByReviewedId(query.ReviewedId, ct);

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