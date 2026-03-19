using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Entities;
using ContrateJa.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContrateJa.Infrastructure.Repositories;

public sealed class ReviewsRepository : IReviewRepository
{
    private readonly AppDbContext _context;

    public ReviewsRepository(AppDbContext context)
        => _context = context;

    public async Task<Review?> GetById(long reviewId, CancellationToken ct = default)
        => await _context.Reviews.FindAsync([reviewId], ct);

    public async Task<IReadOnlyList<Review>> ListByJobId(long jobId, CancellationToken ct = default)
        => await _context.Reviews.Where(x => x.JobId == jobId).ToListAsync(ct);

    public async Task<IReadOnlyList<Review>> ListByReviewerId(long reviewerId, CancellationToken ct = default)
        =>  await _context.Reviews.Where(x => x.ReviewerId == reviewerId).ToListAsync(ct);

    public async Task<IReadOnlyList<Review>> ListByReviewedId(long reviewedId, CancellationToken ct = default)
        => await _context.Reviews.Where(x => x.ReviewedId == reviewedId).ToListAsync(ct);

    public async Task<double> GetAverageRatingByReviewedId(long reviewedId, CancellationToken ct = default)
        => await _context.Reviews.Where(x => x.ReviewedId == reviewedId)
            .AverageAsync(x => x.Rating, ct);

    public async Task Add(Review review, CancellationToken ct = default)
        => await _context.Reviews.AddAsync(review, ct);

    public Task Remove(Review review, CancellationToken ct = default)
    {
        _context.Reviews.Remove(review);
        return Task.CompletedTask;
    }
}