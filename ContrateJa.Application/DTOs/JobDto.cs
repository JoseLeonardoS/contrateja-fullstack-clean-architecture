using ContrateJa.Domain.Enums;
using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Application.DTOs;

public record JobDto(
    long Id,
    long ContractorId,
    string Title,
    string Description,
    State State,
    City City,
    Street Street,
    ZipCode ZipCode,
    EJobStatus Status,
    DateTime CreatedAt,
    DateTime UpdatedAt);