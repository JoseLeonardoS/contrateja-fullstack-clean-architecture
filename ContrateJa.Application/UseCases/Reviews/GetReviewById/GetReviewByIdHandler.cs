using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.UseCases.Shared;
using ContrateJa.Domain.Entities;
using ContrateJa.Domain.Exceptions;
using MediatR;

namespace ContrateJa.Application.UseCases.Reviews.GetReviewById;

public sealed class GetReviewByIdHandler : IRequestHandler<GetReviewByIdQuery, ReviewResponse>
{
  private readonly IReviewRepository _reviewRepository;

  public GetReviewByIdHandler(IReviewRepository reviewRepository)
    => _reviewRepository = reviewRepository;

  public async Task<ReviewResponse> Handle(GetReviewByIdQuery query, CancellationToken ct = default)
  {
    var review = await _reviewRepository.GetById(query.Id, ct);

    if (review is null)
      throw new NotFoundException(nameof(Review), query.Id);

    return new ReviewResponse(
      review.Id,
      review.ReviewerId,
      review.ReviewedId,
      review.JobId,
      review.Rating,
      review.Comment,
      review.CreatedAt,
      review.UpdatedAt,
      review.SubmittedAt);
  }
}