namespace Domain.Models;

public class User
{
    public Guid Id { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public Guid? InvitedById { get; set; }
    public UserLevel Level { get; set; }
}

[Flags]
public enum UserLevel
{
    BaseUser = 1,
    Admin = 2,
    Root = 4
}