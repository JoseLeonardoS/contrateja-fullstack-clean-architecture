using ContrateJa.Application.Abstractions.Repositories;

namespace ContrateJa.Application.UseCases.Reviews.ChangeReviewRating;

public sealed class ChangeReviewRatingHandler
{
  private readonly IReviewRepository _reviewRepository;
  private readonly IUnitOfWork _unitOfWork;

  public ChangeReviewRatingHandler(
    IReviewRepository reviewRepository,
    IUnitOfWork unitOfWork)
  {
    _reviewRepository = reviewRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task Execute(ChangeReviewRatingCommand command, CancellationToken ct = default)
  {
    if (command is null)
      throw new ArgumentNullException(nameof(command));

    if (command.ReviewId <= 0)
      throw new ArgumentOutOfRangeException(nameof(command.ReviewId));

    var review = await _reviewRepository.GetById(command.ReviewId, ct);

    if (review is null)
      throw new InvalidOperationException("Review not found.");

    review.ChangeRating(command.NewRating);

    await _unitOfWork.SaveChanges(ct);
  }
}