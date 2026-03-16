using MediatR;

namespace ContrateJa.Application.UseCases.Users.ChangePassword;

public sealed class ChangePasswordCommand : IRequest
{
    public long UserId { get; }
    public string OldPassword { get; }
    public string NewPassword { get; }
    public string ConfirmPassword { get; }
    
    public ChangePasswordCommand(long userId, string oldPassword, string newPassword, string confirmPassword)
    {
        UserId = userId > 0 ? userId 
                : throw new ArgumentOutOfRangeException(nameof(userId));
        OldPassword = oldPassword;
        NewPassword = newPassword;
        ConfirmPassword = confirmPassword;
    }
}