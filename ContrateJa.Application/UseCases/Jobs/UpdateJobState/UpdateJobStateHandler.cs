using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Application.UseCases.Jobs.UpdateJobState;

public sealed class UpdateJobStateHandler
{
    private readonly IJobRepository _jobRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateJobStateHandler(
        IJobRepository jobRepository,
        IUnitOfWork unitOfWork)
    {
        _jobRepository = jobRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(UpdateJobStateCommand command, CancellationToken ct = default)
    {
        if(command is null)
            throw new ArgumentNullException(nameof(command));
        
        if(command.JobId <= 0)
            throw  new ArgumentOutOfRangeException(nameof(command.JobId));
        
        var job = await _jobRepository.GetById(command.JobId, ct);
        
        if(job is null)
            throw new InvalidOperationException("Job not found.");
        
        job.UpdateState(command.NewState);

        await _unitOfWork.SaveChanges(ct);
    }
}