namespace ContrateJa.Application.UseCases.FreelancerAreas.Shared;

public record FreelancerAreaResponse(
    long Id,
    long FreelancerId,
    string State,
    string City,
    DateTime CreatedAt);