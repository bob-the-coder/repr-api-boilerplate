using Entity = DataAccess.Entities.User;
using Model = Domain.Models.User;

namespace ServiceContracts.Mappers;

public static partial class Mapper
{
    public static Model? UserToModel(Entity? entity)
    {
        if (entity is null) return null;
        
        return new Model
        {
            Id = entity.Id,
            FullName = entity.FullName,
            Email = entity.Email,
            Level = entity.Level,
            InvitedById = entity.InvitedById
        };
    }

    public static Entity? UserToEntity(Model? model)
    {
        if (model is null) return null;
        
        return new Entity
        {
            Id = model.Id,
            FullName = model.FullName,
            Email = model.Email,
            Level = model.Level,
            InvitedById = model.InvitedById
        };
    }
}