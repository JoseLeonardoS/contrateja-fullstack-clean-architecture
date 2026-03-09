using ContrateJa.Application.Abstractions.Repositories;

namespace ContrateJa.Application.UseCases.Reviews.ListReviewsByJob;

public sealed class ListReviewsByJobHandler
{
  private readonly IReviewRepository _reviewRepository;

  public ListReviewsByJobHandler(IReviewRepository reviewRepository)
    => _reviewRepository = reviewRepository;

  public async Task Execute(ListReviewsByJobQuery query, CancellationToken ct = default)
  {
    if (query is null)
      throw new ArgumentNullException(nameof(query));

    if (query.JobId <= 0)
      throw new ArgumentOutOfRangeException(nameof(query.JobId));

    var list = await _reviewRepository.ListByJobId(query.JobId, ct);
  }
}