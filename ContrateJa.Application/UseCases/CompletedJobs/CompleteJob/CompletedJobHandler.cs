using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Entities;
using MediatR;

namespace ContrateJa.Application.UseCases.CompletedJobs.CompleteJob;

public sealed class CompleteJobHandler : IRequestHandler<CompleteJobCommand>
{
    private readonly ICompletedJobRepository _completedJobRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CompleteJobHandler(ICompletedJobRepository completedJobRepository, IUnitOfWork unitOfWork)
    {
        _completedJobRepository = completedJobRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CompleteJobCommand command, CancellationToken ct = default)
    {
        var completedJob = CompletedJob.Create(command.JobId, command.FreelancerId, command.ContractorId);
        await _completedJobRepository.Add(completedJob, ct);
        await _unitOfWork.SaveChanges(ct);
    }
}