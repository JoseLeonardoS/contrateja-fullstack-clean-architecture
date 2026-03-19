using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Entities;
using ContrateJa.Domain.Exceptions;
using FluentValidation;
using MediatR;

namespace ContrateJa.Application.UseCases.Reviews.EditReview;

public sealed class EditReviewHandler : IRequestHandler<EditReviewCommand>
{
  private readonly IReviewRepository _reviewRepository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IValidator<EditReviewCommand> _validator;

  public EditReviewHandler(
    IReviewRepository reviewRepository,
    IUnitOfWork unitOfWork,
    IValidator<EditReviewCommand> validator)
  {
    _reviewRepository = reviewRepository;
    _unitOfWork = unitOfWork;
    _validator = validator;
  }

  public async Task Handle(EditReviewCommand command, CancellationToken ct = default)
  {
    var result  = await _validator.ValidateAsync(command, ct);
    if (!result.IsValid)
      throw new ValidationException(result.Errors);

    var review = await _reviewRepository.GetById(command.ReviewId, ct);
    if (review is null)
      throw new NotFoundException(nameof(Review), command.ReviewId);

    review.EditReview(command.Rating, command.Comment);

    await _unitOfWork.SaveChanges(ct);
  }
}