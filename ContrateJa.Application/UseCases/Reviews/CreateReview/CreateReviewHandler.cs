using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Entities;
using FluentValidation;
using MediatR;

namespace ContrateJa.Application.UseCases.Reviews.CreateReview;

public sealed class CreateReviewHandler : IRequestHandler<CreateReviewCommand>
{
  private readonly IReviewRepository _reviewRepository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IValidator<CreateReviewCommand> _validator;

  public CreateReviewHandler(
    IReviewRepository reviewRepository,
    IUnitOfWork unitOfWork,
    IValidator<CreateReviewCommand> validator)
  {
    _reviewRepository = reviewRepository;
    _unitOfWork = unitOfWork;
    _validator = validator;
  }

  public async Task Handle(CreateReviewCommand command, CancellationToken ct = default)
  {
    var result  = await _validator.ValidateAsync(command, ct);
    if (!result.IsValid)
      throw new ValidationException(result.Errors);

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