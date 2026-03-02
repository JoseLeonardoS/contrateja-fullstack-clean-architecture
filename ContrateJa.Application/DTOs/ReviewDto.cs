namespace ContrateJa.Application.DTOs;

public record ReviewDto(
  long Id,
  long ReviewerId,
  long ReviewedId,
  long JobId,
  int Rating,
  string Comment,
  DateTime SubmittedAt,
  DateTime UpdatedAt
);