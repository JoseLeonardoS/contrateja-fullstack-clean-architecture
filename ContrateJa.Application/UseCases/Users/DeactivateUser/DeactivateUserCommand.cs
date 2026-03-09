namespace ContrateJa.Application.UseCases.Users.DeactivateUser;

public sealed class DeactivateUserCommand
{
    public long UserId { get; }

    public DeactivateUserCommand(long userId)
        => UserId = userId > 0 ? userId 
            : throw new ArgumentOutOfRangeException(nameof(userId));
}