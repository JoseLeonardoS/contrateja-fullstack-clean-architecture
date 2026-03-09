namespace ContrateJa.Application.UseCases.Users.Shared;

public sealed class UserResponse(
    long id,
    string firstName,
    string lastName,
    string email,
    string accountType,
    bool isAvailable,
    string state,
    string city,
    DateTime createdAt)
{
    public long Id { get; } = id;
    public string FirstName { get; } = firstName;
    public string LastName { get; } = lastName;
    public string Email { get; } = email;
    public string AccountType { get; } = accountType;
    public bool IsAvailable { get; } = isAvailable;
    public string State { get; } = state;
    public string City { get; } = city;
    public DateTime CreatedAt { get; } = createdAt;
}