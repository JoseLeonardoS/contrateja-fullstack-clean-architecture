using ContrateJa.Application.UseCases.Jobs.Shared;
using MediatR;

namespace ContrateJa.Application.UseCases.Jobs.ListJobs;

public sealed class ListJobsQuery : IRequest<IReadOnlyList<JobResponse>> { }