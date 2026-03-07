namespace ContrateJa.Application.UseCases.Users.ReactivateUser;

public sealed class ReactivateUserCommand
{
    public long UserId { get; }

    public ReactivateUserCommand(long userId)
        => UserId  =  userId;
}