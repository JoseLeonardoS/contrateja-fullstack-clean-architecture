using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Enums;

namespace ContrateJa.Application.UseCases.Jobs.CloseJob;

public sealed class CloseJobHandler
{
    private readonly IJobRepository _jobRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CloseJobHandler(IJobRepository jobRepository, IUnitOfWork unitOfWork)
    {
        _jobRepository = jobRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(CloseJobCommand command, CancellationToken ct = default)
    {
        if (command is null)
            throw new ArgumentNullException(nameof(command));

        if (command.JobId <= 0)
            throw new ArgumentOutOfRangeException(nameof(command.JobId));

        var job = await _jobRepository.GetById(command.JobId, ct);

        if (job is null)
            throw new InvalidOperationException("Job not found.");

        job.UpdateStatus(EJobStatus.Closed);

        await _unitOfWork.SaveChanges(ct);
    }
}