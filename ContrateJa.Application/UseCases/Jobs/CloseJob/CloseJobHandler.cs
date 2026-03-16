using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Application.UseCases.Users.RegisterUser;
using ContrateJa.Domain.Entities;
using ContrateJa.Domain.Enums;
using ContrateJa.Domain.Exceptions;
using FluentValidation;
using MediatR;

namespace ContrateJa.Application.UseCases.Jobs.CloseJob;

public sealed class CloseJobHandler : IRequestHandler<CloseJobCommand>
{
    private readonly IJobRepository _jobRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CloseJobHandler(
        IJobRepository jobRepository, 
        IUnitOfWork unitOfWork)
    {
        _jobRepository = jobRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CloseJobCommand command, CancellationToken ct = default)
    {
        var job = await _jobRepository.GetById(command.JobId, ct);

        if (job is null)
            throw new NotFoundException(nameof(Job), command.JobId);

        job.UpdateStatus(EJobStatus.Closed);

        await _unitOfWork.SaveChanges(ct);
    }
}