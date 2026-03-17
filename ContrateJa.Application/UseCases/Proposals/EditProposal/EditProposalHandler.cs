using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Entities;
using ContrateJa.Domain.Exceptions;
using ContrateJa.Domain.ValueObjects;
using FluentValidation;
using MediatR;

namespace ContrateJa.Application.UseCases.Proposals.EditProposal;

public sealed class EditProposalHandler : IRequestHandler<EditProposalCommand>
{
    private readonly IProposalRepository _proposalRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<EditProposalCommand> _validator;

    public EditProposalHandler(
        IProposalRepository proposalRepository,
        IUnitOfWork unitOfWork,
        IValidator<EditProposalCommand> validator)
    {
        _proposalRepository = proposalRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task Handle(EditProposalCommand command, CancellationToken ct = default)
    {
        var result = await _validator.ValidateAsync(command, ct);
        if (!result.IsValid)
            throw new  ValidationException(result.Errors);
        
        var proposal = await _proposalRepository.GetById(command.ProposalId, ct);
        if (proposal is null)
            throw new NotFoundException(nameof(Proposal), command.ProposalId);

        proposal.EditProposal(new Money(command.Amount, command.Currency), command.CoverLetter);

        await _unitOfWork.SaveChanges(ct);
    }
}