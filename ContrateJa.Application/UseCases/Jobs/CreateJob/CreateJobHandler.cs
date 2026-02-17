using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Entities;

namespace ContrateJa.Application.UseCases.Jobs.CreateJob;

public sealed class CreateJobHandler
{
    private readonly IJobRepository _jobRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateJobHandler(
        IJobRepository jobRepository,
        IUnitOfWork unitOfWork)
    {
        _jobRepository = jobRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(CreateJobCommand command, CancellationToken ct = default)
    {
        if(command is null)
            throw new ArgumentNullException(nameof(command));

        var newJob = Job.Create(
            command.ContractorId, 
            command.Title, 
            command.Description, 
            command.State, 
            command.City, 
            command.Street, 
            command.ZipCode);
        
        await _jobRepository.Add(newJob, ct);
        await _unitOfWork.SaveChanges(ct);
    }
}