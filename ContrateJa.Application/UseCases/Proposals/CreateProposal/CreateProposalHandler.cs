using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Entities;
using ContrateJa.Domain.ValueObjects;
using FluentValidation;
using MediatR;

namespace ContrateJa.Application.UseCases.Proposals.CreateProposal;

public sealed class CreateProposalHandler : IRequestHandler<CreateProposalCommand>
{
    private readonly IProposalRepository _proposalRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateProposalCommand> _validator;

    public CreateProposalHandler(
        IProposalRepository proposalRepository,
        IUnitOfWork unitOfWork,
        IValidator<CreateProposalCommand> validator)
    {
        _proposalRepository = proposalRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task Handle(CreateProposalCommand command, CancellationToken ct = default)
    {
        var result = await _validator.ValidateAsync(command, ct);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);

        var proposal = Proposal.Create(
                command.JobId,
                command.FreelancerId,
                new Money(command.Amount, command.Currency),
                command.CoverLetter);

        await _proposalRepository.Add(proposal, ct);
        await _unitOfWork.SaveChanges(ct);
    }
}