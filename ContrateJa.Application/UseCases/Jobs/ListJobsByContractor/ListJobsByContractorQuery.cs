using ContrateJa.Application.UseCases.Jobs.Shared;
using MediatR;

namespace ContrateJa.Application.UseCases.Jobs.ListJobsByContractor;

public sealed class ListJobsByContractorQuery : IRequest<IReadOnlyList<JobResponse>>
{
    public long ContractorId { get; }
    
    public ListJobsByContractorQuery(long contractorId)
        => ContractorId = contractorId > 0 ? contractorId 
            : throw new ArgumentOutOfRangeException(nameof(contractorId));
}