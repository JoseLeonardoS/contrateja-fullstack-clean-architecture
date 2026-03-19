namespace ContrateJa.Application.UseCases.Shared;

public record ReviewResponse(
    long Id,
    long ReviewerId,
    long ReviewedId,
    long JobId,
    int Raging,
    string Comment,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    DateTime SubmittedAt);