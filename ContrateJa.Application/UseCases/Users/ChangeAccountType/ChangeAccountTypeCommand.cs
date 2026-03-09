namespace ContrateJa.Application.UseCases.Users.ChangeAccountType;

public sealed class ChangeAccountTypeCommand
{
    public long UserId { get; }
    public string NewAccountType { get; }

    public ChangeAccountTypeCommand(long userId, string newAccountType)
    {
        UserId = userId >  0 ? userId 
            : throw new ArgumentOutOfRangeException(nameof(userId));
        NewAccountType = newAccountType;
    }
}