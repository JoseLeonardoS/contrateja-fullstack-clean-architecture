using ContrateJa.Domain.Entities;

namespace ContrateJa.Application.Abstractions.Repositories;

public interface IReviewRepository
{
  Task<Review?> GetById(long reviewId, CancellationToken ct = default);
  Task<IReadOnlyList<Review>> ListByJobId(long jobId, CancellationToken ct = default);
  Task<IReadOnlyList<Review>> ListByReviewerId(long reviewerId, CancellationToken ct = default);
  Task<IReadOnlyList<Review>> ListByReviewedId(long reviewedId, CancellationToken ct = default);
  Task<double> GetAverageRatingByReviewedId(long reviewedId, CancellationToken ct = default);
  Task Add(Review review, CancellationToken ct = default);
  Task Remove(Review review, CancellationToken ct = default);
}