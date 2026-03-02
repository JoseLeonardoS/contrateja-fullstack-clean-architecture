using ContrateJa.Application.Abstractions.Repositories;

namespace ContrateJa.Application.UseCases.Reviews.GetAverageRatingByUser;

public sealed class GetAverageRatingByUserHandler
{
  private readonly IReviewRepository _reviewRepository;

  public GetAverageRatingByUserHandler(IReviewRepository reviewRepository)
    => _reviewRepository = reviewRepository;

  public async Task<double> Execute(GetAverageRatingByUserQuery query, CancellationToken ct = default)
  {
    if (query is null)
      throw new ArgumentNullException(nameof(query));

    if (query.ReviewedId <= 0)
      throw new ArgumentOutOfRangeException(nameof(query.ReviewedId));

    return await _reviewRepository.GetAverageRatingByReviewedId(query.ReviewedId, ct);
  }
}