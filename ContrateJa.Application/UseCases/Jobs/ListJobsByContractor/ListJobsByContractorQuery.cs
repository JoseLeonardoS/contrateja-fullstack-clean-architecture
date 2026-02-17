namespace ContrateJa.Application.UseCases.Jobs.ListJobsByContractor;

public sealed class ListJobsByContractorQuery
{
    public long ContractorId { get; }
    
    ListJobsByContractorQuery(long contractorId)
        => ContractorId = contractorId;
}