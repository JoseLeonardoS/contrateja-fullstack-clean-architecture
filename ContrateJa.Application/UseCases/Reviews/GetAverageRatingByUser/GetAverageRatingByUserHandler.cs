using ContrateJa.Application.Abstractions.Repositories;
using MediatR;

namespace ContrateJa.Application.UseCases.Reviews.GetAverageRatingByUser;

public sealed class GetAverageRatingByUserHandler : IRequestHandler<GetAverageRatingByUserQuery, double>
{
  private readonly IReviewRepository _reviewRepository;

  public GetAverageRatingByUserHandler(IReviewRepository reviewRepository)
    => _reviewRepository = reviewRepository;

  public async Task<double> Handle(GetAverageRatingByUserQuery query, CancellationToken ct = default)
  {
    return await _reviewRepository.GetAverageRatingByReviewedId(query.ReviewedId, ct);
  }
}