using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Entities;

namespace ContrateJa.Application.UseCases.FreelancerAreas.CreateFreelancerArea;

public sealed class CreateFreelancerAreaHandler
{
    private readonly IFreelancerAreaRepository _freelancerAreaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateFreelancerAreaHandler(
        IFreelancerAreaRepository freelancerAreaRepository,
        IUnitOfWork unitOfWork)
    {
        _freelancerAreaRepository = freelancerAreaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(CreateFreelancerAreaCommand command, CancellationToken ct = default)
    {
        if (command is null)
            throw new ArgumentNullException(nameof(command));

        if (command.FreelancerId <= 0)
            throw new ArgumentOutOfRangeException(nameof(command.FreelancerId));

        if (command.Area is null)
            throw new ArgumentNullException(nameof(command.Area));

        if (await _freelancerAreaRepository.Exists(command.FreelancerId, command.Area, ct))
            throw new InvalidOperationException("Freelancer area already exists.");
        
        var freelancerArea = FreelancerArea.Create(command.FreelancerId, command.Area);
        
        await _freelancerAreaRepository.Add(freelancerArea, ct);
        await _unitOfWork.SaveChanges(ct);
    }
}