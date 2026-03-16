using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Entities;
using ContrateJa.Domain.Enums;
using ContrateJa.Domain.Exceptions;
using FluentValidation;
using MediatR;

namespace ContrateJa.Application.UseCases.Jobs.UpdateJobStatus;

public sealed class UpdateJobStatusHandler : IRequestHandler<UpdateJobStatusCommand>
{
    private readonly IJobRepository _jobRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateJobStatusCommand> _validator;

    public UpdateJobStatusHandler(
        IJobRepository jobRepository,
        IUnitOfWork unitOfWork,
        IValidator<UpdateJobStatusCommand> validator)
    {
        _jobRepository = jobRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task Handle(UpdateJobStatusCommand command, CancellationToken ct = default)
    {
        var result = await _validator.ValidateAsync(command, ct);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);
        
        var job =  await _jobRepository.GetById(command.JobId, ct);
        if(job is null)
            throw new NotFoundException(nameof(Job), command.JobId);

        job.UpdateStatus(Enum.Parse<EJobStatus>(command.NewStatus));

        await _unitOfWork.SaveChanges(ct);
    }
}