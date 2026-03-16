using MediatR;

namespace ContrateJa.Application.UseCases.Users.ReactivateUser;

public sealed class ReactivateUserCommand : IRequest
{
    public long UserId { get; }

    public ReactivateUserCommand(long userId)
        => UserId  =  userId > 0 
            ? userId 
            : throw new ArgumentOutOfRangeException(nameof(userId));
}