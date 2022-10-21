using System.IdentityModel.Tokens.Jwt;
using Domain.JsonModels;
using Newtonsoft.Json;
using Entity = DataAccess.Entities.BlogPost;
using Model = Domain.Models.BlogPost;

namespace ServiceContracts.Mappers;

public static partial class Mapper
{
    public static Model BlogPostToModel(Entity entity)
    {
        return new Model
        {
            Id = entity.Id,
            Title = entity.Title,
            CreatedById = entity.CreatedById,
            CreatedOnUtc = entity.CreatedOnUtc,
            UpdatedById = entity.UpdatedById,
            UpdatedOnUtc = entity.UpdatedOnUtc,
            PublishedOnUtc = entity.PublishedOnUtc,
            Content = JsonConvert.DeserializeObject<ViewContentContainer>(entity.ContentJson)
        };
    }

    public static Entity BlogPostToEntity(Model model)
    {
        return new Entity
        {
            Id = model.Id,
            Title = model.Title,
            CreatedById = model.CreatedById,
            CreatedOnUtc = model.CreatedOnUtc,
            UpdatedById = model.UpdatedById,
            UpdatedOnUtc = model.UpdatedOnUtc,
            PublishedOnUtc = model.PublishedOnUtc,
            ContentJson = JsonConvert.SerializeObject(model.Content)
        };
    }
}