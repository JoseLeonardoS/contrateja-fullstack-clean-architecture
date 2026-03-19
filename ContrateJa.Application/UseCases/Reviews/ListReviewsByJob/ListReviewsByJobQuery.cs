using ContrateJa.Application.UseCases.Shared;
using MediatR;

namespace ContrateJa.Application.UseCases.Reviews.ListReviewsByJob;

public sealed class ListReviewsByJobQuery : IRequest<IReadOnlyList<ReviewResponse>>
{
  public long JobId { get; }

  public ListReviewsByJobQuery(long jobId)
    => JobId = jobId > 0 ? jobId 
      : throw new ArgumentOutOfRangeException(nameof(jobId));
}