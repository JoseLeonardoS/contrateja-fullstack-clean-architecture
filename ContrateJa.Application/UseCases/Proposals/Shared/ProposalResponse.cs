namespace ContrateJa.Application.UseCases.Proposals.Shared;

public record ProposalResponse(
    long Id,
    long JobId,
    long FreelancerId,
    decimal Amount,
    string Currency,
    string CoverLetter,
    string Status,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    DateTime SubmittedAt);