using ContrateJa.Application.Abstractions.Repositories;

namespace ContrateJa.Application.UseCases.FreelancerAreas.RemoveFreelancerArea;

public sealed class RemoveFreelancerAreaHandler
{
    private readonly IFreelancerAreaRepository _freelancerAreaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveFreelancerAreaHandler(
        IFreelancerAreaRepository freelancerAreaRepository, 
        IUnitOfWork unitOfWork)
    {
        _freelancerAreaRepository = freelancerAreaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(
        RemoveFreelancerAreaCommand command, 
        CancellationToken ct = default)
    {
        if(command is null)
            throw new ArgumentNullException(nameof(command));
        
        if(command.FreelancerAreaId <= 0)
            throw new ArgumentOutOfRangeException(nameof(command.FreelancerAreaId));

        var freelancerArea = await _freelancerAreaRepository.GetById(command.FreelancerAreaId, ct);
        
        if(freelancerArea == null)
            throw new InvalidOperationException("Freelancer area not found.");
        
        await _freelancerAreaRepository.Remove(command.FreelancerAreaId, ct);
        await _unitOfWork.SaveChanges(ct);
    }
}
