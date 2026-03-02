using ContrateJa.Application.Abstractions.Repositories;

namespace ContrateJa.Application.UseCases.Reviews.EditReviewComment;

public sealed class EditReviewCommentHandler
{
  private readonly IReviewRepository _reviewRepository;
  private readonly IUnitOfWork _unitOfWork;

  public EditReviewCommentHandler(
    IReviewRepository reviewRepository,
    IUnitOfWork unitOfWork)
  {
    _reviewRepository = reviewRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task Execute(EditReviewCommentCommand command, CancellationToken ct = default)
  {
    if (command is null)
      throw new ArgumentNullException(nameof(command));

    if (command.ReviewId <= 0)
      throw new ArgumentOutOfRangeException(nameof(command.ReviewId));

    var review = await _reviewRepository.GetById(command.ReviewId, ct);

    if (review is null)
      throw new InvalidOperationException("Review not found.");

    review.EditComment(command.NewComment);

    await _unitOfWork.SaveChanges(ct);
  }
}