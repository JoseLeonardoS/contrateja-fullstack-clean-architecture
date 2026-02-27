namespace ContrateJa.Application.UseCases.Proposals.ListProposalsByJob;

public sealed class ListProposalsByJobQuery
{
  public long JobId { get; }

  public ListProposalsByJobQuery(long jobId)
    => JobId = jobId;
}