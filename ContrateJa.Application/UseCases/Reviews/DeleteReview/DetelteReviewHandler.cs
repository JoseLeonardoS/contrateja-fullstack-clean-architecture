using ContrateJa.Application.Abstractions.Repositories;

namespace ContrateJa.Application.UseCases.Reviews.DeleteReview;

public sealed class DeleteReviewHandler
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

  public async Task Execute(DeleteReviewCommand command, CancellationToken ct = default)
  {
    if (command is null)
      throw new ArgumentNullException(nameof(command));

    if (command.ReviewId <= 0)
      throw new ArgumentOutOfRangeException(nameof(command.ReviewId));

    var review = await _reviewRepository.GetById(command.ReviewId, ct);

    if (review is null)
      throw new InvalidOperationException("Review not found.");

    await _reviewRepository.Remove(review, ct);
    await _unitOfWork.SaveChanges(ct);
  }
}