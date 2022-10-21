using DataAccessExtensions.Contract;
using Domain.Models;

namespace DataAccess.Entities;

public class User : IPkEntity<Guid>
{
    public Guid Id { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public Guid? InvitedById { get; set; }
    public UserLevel Level { get; set; }
}
