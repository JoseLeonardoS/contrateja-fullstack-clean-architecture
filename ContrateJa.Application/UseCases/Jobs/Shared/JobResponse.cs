namespace ContrateJa.Application.UseCases.Jobs.Shared;

public record JobResponse(
    long Id,
    long ContractorId,
    string Title,
    string Description,
    string State,
    string City,
    string Street,
    string ZipCode,
    string Status,
    DateTime CreatedAt,
    DateTime UpdatedAt
);