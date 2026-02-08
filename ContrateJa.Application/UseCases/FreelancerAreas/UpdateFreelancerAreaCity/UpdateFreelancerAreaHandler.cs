using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Entities;
using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Application.UseCases.FreelancerAreas.UpdateFreelancerAreaCity;

public sealed class UpdateFreelancerAreaHandler
{
    private readonly IFreelancerAreaRepository _freelancerAreaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateFreelancerAreaHandler(
        IFreelancerAreaRepository freelancerAreaRepository, 
        IUnitOfWork unitOfWork)
    {
        _freelancerAreaRepository = freelancerAreaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(UpdateFreelancerAreaCityCommand command, CancellationToken ct = default)
    {
        if (command is null)
            throw new ArgumentNullException(nameof(command));

        if (command.FreelancerAreaId <= 0)
            throw new ArgumentOutOfRangeException(nameof(command));
        
        var city = await _freelancerAreaRepository.GetById(command.FreelancerAreaId);
        
        if(city is null)
            throw new ArgumentNullException(nameof(city));
        
        // TODO: Implement remaining busines rules and persistence logic
    }
}