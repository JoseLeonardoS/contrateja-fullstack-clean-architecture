using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Entities;

namespace ContrateJa.Application.UseCases.Proposals.CreateProposal;

public sealed class CreateProposalHandler
{
    private readonly IProposalRepository _proposalRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProposalHandler(
        IProposalRepository proposalRepository,
        IUnitOfWork unitOfWork)
    {
        _proposalRepository = proposalRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(CreateProposalCommand command, CancellationToken ct = default)
    {
        if(command is null)
            throw new ArgumentNullException(nameof(command));

        var proposal = Proposal.Create(
                command.JobId,
                command.FreelancerId,
                command.Amount,
                command.CoverLetter);

        await _proposalRepository.Add(proposal, ct);
        await _unitOfWork.SaveChanges(ct);
    }
}