using ContrateJa.Application.Abstractions.Repositories;

namespace ContrateJa.Application.UseCases.Reviews.ListReviewsByReviewerUser;

public sealed class ListReviewsByReviewerUserHandler
{
  private readonly IReviewRepository _reviewRepository;

  public ListReviewsByReviewerUserHandler(IReviewRepository reviewRepository)
    => _reviewRepository = reviewRepository;

  public async Task Execute(ListReviewsByReviewerUserQuery query, CancellationToken ct = default)
  {
    if (query is null)
      throw new ArgumentNullException(nameof(query));

    if (query.ReviewerId <= 0)
      throw new ArgumentOutOfRangeException(nameof(query.ReviewerId));

    var list = await _reviewRepository.ListByReviewerId(query.ReviewerId, ct);
  }
}