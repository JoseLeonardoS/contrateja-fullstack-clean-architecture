using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Entities;
using ContrateJa.Domain.Exceptions;
using MediatR;

namespace ContrateJa.Application.UseCases.Reviews.DeleteReview;

public sealed class DeleteReviewHandler : IRequestHandler<DeleteReviewCommand>
{
  private readonly IReviewRepository _reviewRepository;
  private readonly IUnitOfWork _unitOfWork;

  public DeleteReviewHandler(
    IReviewRepository reviewRepository,
    IUnitOfWork unitOfWork)
  {
    _reviewRepository = reviewRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task Handle(DeleteReviewCommand command, CancellationToken ct = default)
  {
    var review = await _reviewRepository.GetById(command.ReviewId, ct);
    if (review is null)
      throw new NotFoundException(nameof(Review), command.ReviewId);

    await _reviewRepository.Remove(review, ct);
    await _unitOfWork.SaveChanges(ct);
  }
}