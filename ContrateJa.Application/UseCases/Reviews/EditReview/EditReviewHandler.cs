using ContrateJa.Application.Abstractions.Repositories;

namespace ContrateJa.Application.UseCases.Reviews.EditReview;

public sealed class EditReviewHandler
{
  private readonly IReviewRepository _reviewRepository;
  private readonly IUnitOfWork _unitOfWork;

  public EditReviewHandler(
    IReviewRepository reviewRepository,
    IUnitOfWork unitOfWork)
  {
    _reviewRepository = reviewRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task Execute(EditReviewCommand command, CancellationToken ct = default)
  {
    if (command is null)
      throw new ArgumentNullException(nameof(command));

    if (command.ReviewId <= 0)
      throw new ArgumentOutOfRangeException(nameof(command.ReviewId));

    var review = await _reviewRepository.GetById(command.ReviewId, ct);

    if (review is null)
      throw new InvalidOperationException("Review not found.");

    review.EditReview(command.NewRating, command.NewComment);

    await _unitOfWork.SaveChanges(ct);
  }
}