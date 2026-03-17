using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Entities;
using ContrateJa.Domain.Exceptions;
using MediatR;

namespace ContrateJa.Application.UseCases.Proposals.DeleteProposal;

public sealed class DeleteProposalHandler : IRequestHandler<DeleteProposalCommand>
{
    private readonly IProposalRepository _proposalRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProposalHandler(
        IProposalRepository proposalRepository,
        IUnitOfWork unitOfWork)
    {
        _proposalRepository = proposalRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteProposalCommand command, CancellationToken ct = default)
    {
        var proposal = await _proposalRepository.GetById(command.ProposalId, ct);
        if (proposal is null)
            throw new NotFoundException(nameof(Proposal), command.ProposalId);
        
        await _proposalRepository.Remove(proposal, ct);
        await _unitOfWork.SaveChanges(ct);
    }
}