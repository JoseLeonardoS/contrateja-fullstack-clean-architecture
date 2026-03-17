using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Exceptions;
using FluentValidation;
using MediatR;

namespace ContrateJa.Application.UseCases.Proposals.EditProposalCoverLetter;

public sealed class EditProposalCoverLetterHandler : IRequestHandler<EditProposalCoverLetterCommand>
{
  private readonly IProposalRepository _proposalRepository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IValidator<EditProposalCoverLetterCommand> _validator;

  public EditProposalCoverLetterHandler(
    IProposalRepository proposalRepository,
    IUnitOfWork unitOfWork,
    IValidator<EditProposalCoverLetterCommand> validator)
  {
    _proposalRepository = proposalRepository;
    _unitOfWork = unitOfWork;
    _validator = validator;
  }

  public async Task Handle(EditProposalCoverLetterCommand command, CancellationToken ct = default)
  {
    var result = await _validator.ValidateAsync(command, ct);
    if (!result.IsValid)
      throw new ValidationException(result.Errors);

    var proposal = await _proposalRepository.GetById(command.ProposalId, ct);
    if (proposal is null)
      throw new NotFoundException(nameof(proposal), command.ProposalId);

    proposal.EditCoverLetter(command.CoverLetter);

    await _unitOfWork.SaveChanges(ct);
  }
}