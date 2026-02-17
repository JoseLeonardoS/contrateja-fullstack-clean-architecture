using ContrateJa.Application.Abstractions.Repositories;

namespace ContrateJa.Application.UseCases.Jobs.UpdateJobStreet;

public sealed class UpdateJobStreetHandler
{
    private readonly IJobRepository _jobRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateJobStreetHandler(
        IJobRepository jobRepository,
        IUnitOfWork unitOfWork)
    {
        _jobRepository = jobRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(UpdateJobStreetCommand command, CancellationToken ct = default)
    {
        if(command is null)
            throw new ArgumentNullException(nameof(command));
        
        if(command.JobId <= 0)
            throw new ArgumentOutOfRangeException(nameof(command.JobId));
        
        var job =  await _jobRepository.GetById(command.JobId, ct);
        
        if(job is null)
            throw new InvalidOperationException("Job not found.");
        
        job.UpdateStreet(command.NewStreet);

        await _unitOfWork.SaveChanges(ct);
    }
}