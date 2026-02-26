using ContrateJa.Application.Abstractions.Repositories;

namespace ContrateJa.Application.UseCases.Proposals.EditProposal;

public sealed class EditProposalHandler
{
    private readonly IProposalRepository _proposalRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditProposalHandler(
        IProposalRepository proposalRepository,
        IUnitOfWork unitOfWork)
    {
        _proposalRepository = proposalRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(EditProposalCommand command, CancellationToken ct = default)
    {
        if (command is null)
            throw new ArgumentNullException(nameof(command));

        if (command.ProposalId <= 0)
            throw new ArgumentOutOfRangeException(nameof(command.ProposalId));

        var proposal = await _proposalRepository.GetById(command.ProposalId, ct);

        if (proposal is null)
            throw new InvalidOperationException("Proposal not found.");

        proposal.EditProposal(command.NewAmount, command.NewCoverLetter);

        await _unitOfWork.SaveChanges(ct);
    }
}