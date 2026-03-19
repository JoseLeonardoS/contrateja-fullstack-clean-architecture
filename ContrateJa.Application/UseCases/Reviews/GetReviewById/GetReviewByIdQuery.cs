using ContrateJa.Application.UseCases.Shared;
using MediatR;

namespace ContrateJa.Application.UseCases.Reviews.GetReviewById;

public sealed class GetReviewByIdQuery : IRequest<ReviewResponse>
{
  public long Id { get; }

  public GetReviewByIdQuery(long id)
    => Id = id > 0 ? id 
        : throw new ArgumentOutOfRangeException(nameof(id));
}