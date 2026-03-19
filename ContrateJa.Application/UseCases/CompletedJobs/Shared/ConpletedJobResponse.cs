namespace ContrateJa.Application.UseCases.CompletedJobs.Shared;

public record CompletedJobResponse(
    long Id,
    long JobId,
    long FreelancerId,
    long ContractorId,
    DateTime CompletedAt,
    DateTime CreatedAt);