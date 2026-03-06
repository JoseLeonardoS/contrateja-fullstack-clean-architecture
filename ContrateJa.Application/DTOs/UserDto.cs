using ContrateJa.Domain.Enums;
using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Application.DTOs;

public record UserDto(
      Name Name,
      Phone Phone,
      Email Email,
      EAccountType AccountType,
      bool IsAvailable,
      Document Document,
      State State,
      City City,
      Street Street,
      ZipCode ZipCode,
      DateTime CreatedAt,
      DateTime UpdatedAt);