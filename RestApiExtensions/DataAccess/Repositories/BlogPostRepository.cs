using DataAccess.Entities;
using DataAccessExtensions.Contract;
using DataAccessExtensions.Implementation;

namespace DataAccess.Repositories;

public interface IBlogPostRepository : IPkRepository<BlogPost, Guid>
{
    Blog[] GetByCreatorId(Guid creatorId);
}

public class BlogPostRepository : DbContextPkRepository<ExampleContext, BlogPost, Guid>, IBlogPostRepository
{
    public BlogPostRepository(ExampleContext dbContext) : base(dbContext)
    {
    }

    public Blog[] GetByCreatorId(Guid creatorId)
    {
        throw new NotImplementedException();
    }
}