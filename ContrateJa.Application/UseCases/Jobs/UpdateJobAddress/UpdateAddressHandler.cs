using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Entities;
using ContrateJa.Domain.Exceptions;
using ContrateJa.Domain.ValueObjects;
using FluentValidation;
using MediatR;

namespace ContrateJa.Application.UseCases.Jobs.UpdateJobAddress;

public sealed class UpdateAddressHandler : IRequestHandler<UpdateAddressCommand>
{
    private readonly IJobRepository _jobRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateAddressCommand> _validator;
    
    public UpdateAddressHandler(IJobRepository jobRepository, 
        IUnitOfWork unitOfWork, 
        IValidator<UpdateAddressCommand> validator)
    {
        _jobRepository = jobRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task Handle(UpdateAddressCommand command, CancellationToken ct = default)
    {
        var result = await _validator.ValidateAsync(command, ct);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);
        
        var job = await _jobRepository.GetById(command.JobId, ct);
        if (job is null)
            throw new NotFoundException(nameof(Job),  command.JobId);
        
        job.UpdateAddress(
            new State(command.State),
            new City(command.City),
            new Street(command.Street),
            new ZipCode(command.ZipCode));

        await _unitOfWork.SaveChanges(ct);
    }
}