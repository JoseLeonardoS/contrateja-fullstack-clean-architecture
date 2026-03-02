using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.DTOs;

namespace ContrateJa.Application.UseCases.Reviews.ListReviewsByJob;

public sealed class ListReviewsByJobHandler
{
  private readonly IReviewRepository _reviewRepository;

  public ListReviewsByJobHandler(IReviewRepository reviewRepository)
    => _reviewRepository = reviewRepository;

  public async Task<IReadOnlyList<ReviewDto>> Execute(ListReviewsByJobQuery query, CancellationToken ct = default)
  {
    if (query is null)
      throw new ArgumentNullException(nameof(query));

    if (query.JobId <= 0)
      throw new ArgumentOutOfRangeException(nameof(query.JobId));

    var list = await _reviewRepository.ListByJobId(query.JobId, ct);

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