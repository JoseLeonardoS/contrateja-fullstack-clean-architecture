using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.DTOs;

namespace ContrateJa.Application.UseCases.Reviews.GetReviewById;

public sealed class GetReviewByIdHandler
{
  private readonly IReviewRepository _reviewRepository;

  public GetReviewByIdHandler(IReviewRepository reviewRepository)
    => _reviewRepository = reviewRepository;

  public async Task<ReviewDto> Execute(GetReviewByIdQuery query, CancellationToken ct = default)
  {
    if (query is null)
      throw new ArgumentNullException(nameof(query));

    if (query.Id <= 0)
      throw new ArgumentOutOfRangeException(nameof(query.Id));

    var review = await _reviewRepository.GetById(query.Id, ct);

    if (review is null)
      throw new InvalidOperationException("Review not found.");

    return new ReviewDto(
      review.Id,
      review.ReviewerId,
      review.ReviewedId,
      review.JobId,
      review.Rating,
      review.Comment,
      review.SubmittedAt,
      review.UpdatedAt);
  }
}