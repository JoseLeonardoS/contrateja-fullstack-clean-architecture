using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Entities;
using ContrateJa.Domain.Exceptions;
using FluentValidation;
using MediatR;

namespace ContrateJa.Application.UseCases.Jobs.UpdateJobTitle;

public sealed class UpdateJobTitleHandler : IRequestHandler<UpdateJobTitleCommand>
{
    private  readonly IJobRepository _jobRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateJobTitleCommand> _validator;

    public UpdateJobTitleHandler(
        IJobRepository jobRepository,
        IUnitOfWork unitOfWork,
        IValidator<UpdateJobTitleCommand> validator)
    {
        _jobRepository = jobRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task Handle(UpdateJobTitleCommand command, CancellationToken ct = default)
    {
        var result = await _validator.ValidateAsync(command, ct);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);
        
        var job = await _jobRepository.GetById(command.JobId, ct);
        
        if(job is null)
            throw new NotFoundException(nameof(Job), command.JobId);

        job.UpdateTitle(command.NewTitle);

        await _unitOfWork.SaveChanges(ct);
    }
}