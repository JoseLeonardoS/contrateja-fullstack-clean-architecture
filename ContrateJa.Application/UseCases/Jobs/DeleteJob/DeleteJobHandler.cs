using ContrateJa.Application.Abstractions.Repositories;

namespace ContrateJa.Application.UseCases.Jobs.DeleteJob;

public sealed class DeleteJobHandler
{
    private readonly IJobRepository _jobRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteJobHandler(
        IJobRepository jobRepository,
        IUnitOfWork unitOfWork)
    {
        _jobRepository = jobRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(DeleteJobCommand command, CancellationToken ct = default)
    {
        if(command is null)
            throw new ArgumentNullException(nameof(command));
        
        if(command.JobId <= 0)
            throw new ArgumentOutOfRangeException(nameof(command.JobId));
        
        var job = await _jobRepository.GetById(command.JobId, ct);
        
        if(job is null)
            throw new InvalidOperationException("Job not found.");
        
        await _jobRepository.Remove(job, ct);
        await _unitOfWork.SaveChanges(ct);
    }
}