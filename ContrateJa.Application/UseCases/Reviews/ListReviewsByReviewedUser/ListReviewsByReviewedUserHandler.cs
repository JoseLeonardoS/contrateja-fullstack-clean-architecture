using ContrateJa.Application.Abstractions.Repositories;

namespace ContrateJa.Application.UseCases.Reviews.ListReviewsByReviewedUser;

public sealed class ListReviewsByReviewedUserHandler
{
  private readonly IReviewRepository _reviewRepository;

  public ListReviewsByReviewedUserHandler(IReviewRepository reviewRepository)
    => _reviewRepository = reviewRepository;

  public async Task Execute(ListReviewsByReviewedUserQuery query, CancellationToken ct = default)
  {
    if (query is null)
      throw new ArgumentNullException(nameof(query));

    if (query.ReviewedId <= 0)
      throw new ArgumentOutOfRangeException(nameof(query.ReviewedId));

    var list = await _reviewRepository.ListByReviewedId(query.ReviewedId, ct);
  }
}