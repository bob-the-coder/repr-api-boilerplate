using Entity = DataAccess.Entities.Blog;
using Model = Domain.Models.Blog;

namespace ServiceContracts.Mappers;

public static partial class Mapper
{
    public static Model BlogToModel(Entity entity)
    {
        return new Model
        {
            Id = entity.Id,
            Name = entity.Name,
            Tags = entity.Tags,
            CreatedById = entity.CreatedById,
            CreatedOnUtc = entity.CreatedOnUtc,
            UpdatedById = entity.UpdatedById,
            UpdatedOnUtc = entity.UpdatedOnUtc,
            Authors = entity.Authors?.Select(UserToModel).Where(user => user is not null).ToList(),
            BlogPosts = entity.BlogPosts?.Select(BlogPostToModel).ToList()
        };
    }

    public static Entity BlogToEntity(Model model)
    {
        return new Entity
        {
            Id = model.Id,
            Name = model.Name,
            Tags = model.Tags,
            CreatedById = model.CreatedById,
            CreatedOnUtc = model.CreatedOnUtc,
            UpdatedById = model.UpdatedById,
            UpdatedOnUtc = model.UpdatedOnUtc,
            Authors = model.Authors?.Select(UserToEntity).Where(user => user is not null).ToList(),
            BlogPosts = model.BlogPosts?.Select(BlogPostToEntity).ToList()
        };
    }
}