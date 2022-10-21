namespace ServiceContracts.Contract;

public static class Constants
{
    public const string ThisIsPrivate = "This is private";
    public static readonly Domain.Models.User Steve = new Domain.Models.User()
    {
        Id = Guid.Parse("02082f35-93a4-4ddf-b80a-5596c8208d78"),
        FullName = "Steve",
        Email = "steve@email.com"
    };
}