using ContrateJa.Application.Abstractions.Repositories;
using ContrateJa.Domain.Entities;
using ContrateJa.Domain.Exceptions;
using MediatR;

namespace ContrateJa.Application.UseCases.FreelancerAreas.RemoveFreelancerArea;

public sealed class RemoveFreelancerAreaHandler : IRequestHandler<RemoveFreelancerAreaCommand>
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

    public async Task Handle(RemoveFreelancerAreaCommand command, 
        CancellationToken ct = default)
    {
        var freelancerArea = await _freelancerAreaRepository.GetById(command.FreelancerAreaId, ct);
        if(freelancerArea is null)
            throw new NotFoundException(nameof(FreelancerArea), command.FreelancerAreaId);
        
        await _freelancerAreaRepository.Remove(freelancerArea, ct);
        await _unitOfWork.SaveChanges(ct);
    }
}
