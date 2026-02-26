using ContrateJa.Domain.Enums;
using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Application.DTOs;

public record ProposalDto(
    long Id, 
    long JobId, 
    long FreelancerId, 
    Money Amount, 
    string CoverLetter, 
    EProposalStatus Status,
    DateTime SubmittedAt, 
    DateTime UpdatedAt);