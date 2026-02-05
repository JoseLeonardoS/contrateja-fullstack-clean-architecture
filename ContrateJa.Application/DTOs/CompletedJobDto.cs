namespace ContrateJa.Application.DTOs;

public record CompletedJobDto
    (
        long Id,
        long JobId,
        long FreelancerId,
        long ContractorId,
        DateTime CompleteAt
    );