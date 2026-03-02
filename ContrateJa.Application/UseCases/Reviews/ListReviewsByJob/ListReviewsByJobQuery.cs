namespace ContrateJa.Application.UseCases.Reviews.ListReviewsByJob;

public sealed class ListReviewsByJobQuery
{
  public long JobId { get; }

  public ListReviewsByJobQuery(long jobId)
    => JobId = jobId;
}