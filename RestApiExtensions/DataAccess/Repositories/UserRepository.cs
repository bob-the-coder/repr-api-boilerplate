using DataAccess.Entities;
using DataAccessExtensions.Contract;
using DataAccessExtensions.Implementation;

namespace DataAccess.Repositories;

public interface IUserRepository : IPkRepository<User, Guid>
{
}

internal sealed class UserRepository : DbContextPkRepository<ExampleContext, User, Guid>, IUserRepository
{
    public UserRepository(ExampleContext dbContext) : base(dbContext)
    {
    }
}