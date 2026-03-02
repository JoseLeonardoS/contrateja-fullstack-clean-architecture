using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Entities;

namespace ContrateJa.Application.UseCases.Reviews.CreateReview;

public sealed class CreateReviewHandler
{
  private readonly IReviewRepository _reviewRepository;
  private readonly IUnitOfWork _unitOfWork;

  public CreateReviewHandler(
    IReviewRepository reviewRepository,
    IUnitOfWork unitOfWork)
  {
    _reviewRepository = reviewRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task Execute(CreateReviewCommand command, CancellationToken ct = default)
  {
    if (command is null)
      throw new ArgumentNullException(nameof(command));

    var review = Review.Create(
      command.ReviewerId,
      command.ReviewedId,
      command.JobId,
      command.Rating,
      command.Comment);

    await _reviewRepository.Add(review, ct);
    await _unitOfWork.SaveChanges(ct);
  }
}