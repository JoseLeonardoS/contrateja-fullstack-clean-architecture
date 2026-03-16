using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Entities;
using ContrateJa.Domain.ValueObjects;
using FluentValidation;
using MediatR;

namespace ContrateJa.Application.UseCases.Jobs.CreateJob;

public sealed class CreateJobHandler : IRequestHandler<CreateJobCommand>
{
    private readonly IJobRepository _jobRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateJobCommand> _validator;

    public CreateJobHandler(
        IJobRepository jobRepository,
        IUnitOfWork unitOfWork,
        IValidator<CreateJobCommand> validator)
    {
        _jobRepository = jobRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task Handle(CreateJobCommand command, CancellationToken ct = default)
    {
        var result = await _validator.ValidateAsync(command, ct);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);

        var newJob = Job.Create(
            command.ContractorId, 
            command.Title, 
            command.Description, 
            new State(command.State),
            new City(command.City), 
            new Street(command.Street), 
            new ZipCode(command.ZipCode));
        
        await _jobRepository.Add(newJob, ct);
        await _unitOfWork.SaveChanges(ct);
    }
}