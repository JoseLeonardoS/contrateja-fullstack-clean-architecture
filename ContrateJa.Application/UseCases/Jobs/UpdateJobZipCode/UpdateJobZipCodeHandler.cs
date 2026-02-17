using ContrateJa.Application.Abstractions.Repositories;

namespace ContrateJa.Application.UseCases.Jobs.UpdateJobZipCode;

public sealed class UpdateJobZipCodeHandler
{
    private readonly IJobRepository _jobRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateJobZipCodeHandler(
        IJobRepository jobRepository,
        IUnitOfWork unitOfWork)
    {
        _jobRepository = jobRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(UpdateJobZipCodeCommand command, CancellationToken ct = default)
    {
        if(command is null)
            throw new ArgumentNullException(nameof(command));
        
        if(command.JobId <= 0)
            throw new ArgumentOutOfRangeException(nameof(command.JobId));
        
        var job =  await _jobRepository.GetById(command.JobId, ct);

        if (job is null)
            throw new InvalidOperationException("Job not found.");
        
        job.UpdateZipCode(command.NewZipCode);

        await _unitOfWork.SaveChanges(ct);
    }
}