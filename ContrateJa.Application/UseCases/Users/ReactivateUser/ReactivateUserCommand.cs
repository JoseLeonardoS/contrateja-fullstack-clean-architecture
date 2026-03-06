namespace ContrateJa.Application.UseCases.Users.ReactivateUser;

public class ReactivateUserCommand
{
    public long UserId { get; }

    public TYPE Type { get; set; }

    public ReactivateUserCommand()
    {
        
    }
} 