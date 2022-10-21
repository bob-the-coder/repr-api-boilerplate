using ServiceContracts.Contract;

namespace ApiExample.User.ResponseModels;

public record UserResponse
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? Email { get; init; }

    public UserResponse(Domain.Models.User user)
    {
        Id = user.Id;
        Name = user.FullName;
        Email = Constants.ThisIsPrivate;
    }
}