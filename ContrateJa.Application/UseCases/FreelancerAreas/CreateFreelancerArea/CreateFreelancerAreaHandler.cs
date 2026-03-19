using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Entities;
using ContrateJa.Domain.Exceptions;
using ContrateJa.Domain.ValueObjects;
using FluentValidation;
using MediatR;

namespace ContrateJa.Application.UseCases.FreelancerAreas.CreateFreelancerArea;

public sealed class CreateFreelancerAreaHandler : IRequestHandler<CreateFreelancerAreaCommand>
{
    private readonly IFreelancerAreaRepository _freelancerAreaRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateFreelancerAreaCommand> _validator;

    public CreateFreelancerAreaHandler(
        IFreelancerAreaRepository freelancerAreaRepository,
        IUnitOfWork unitOfWork,
        IValidator<CreateFreelancerAreaCommand> validator)
    {
        _freelancerAreaRepository = freelancerAreaRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task Handle(CreateFreelancerAreaCommand command, CancellationToken ct = default)
    {
        var result = await _validator.ValidateAsync(command, ct);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);
        
        var area = new Area(new State(command.State), new City(command.City));
        
        if (await _freelancerAreaRepository.Exists(command.FreelancerId, area, ct ))
            throw new ConflictException("Freelancer area already exists.");
        
        var freelancerArea = FreelancerArea.Create(command.FreelancerId, area);
        
        await _freelancerAreaRepository.Add(freelancerArea, ct);
        await _unitOfWork.SaveChanges(ct);
    }
}