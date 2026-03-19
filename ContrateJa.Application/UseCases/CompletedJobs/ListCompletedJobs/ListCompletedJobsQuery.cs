using MediatR;
using ContrateJa.Application.UseCases.CompletedJobs.Shared;

namespace ContrateJa.Application.UseCases.CompletedJobs.ListCompletedJobs;

public sealed class ListCompletedJobsQuery : IRequest<IReadOnlyList<CompletedJobResponse>>;