using DataAccess.Entities;
using DataAccessExtensions.Contract;
using DataAccessExtensions.Implementation;

namespace DataAccess.Repositories;

public interface IBlogRepository : IPkRepository<Blog, Guid>
{
}

internal sealed class BlogRepository : DbContextPkRepository<ExampleContext, Blog, Guid>, IBlogRepository
{
    public BlogRepository(ExampleContext dbContext) : base(dbContext)
    {
    }
}