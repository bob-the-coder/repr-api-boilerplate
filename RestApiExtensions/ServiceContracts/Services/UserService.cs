using DataAccess.Repositories;
using DataAccessExtensions.Contract;
using Domain.Models;
using ServiceContracts.Contract;
using ServiceContracts.Mappers;
using ServiceContracts.Providers;

namespace ServiceContracts.Services;

public interface IUserService
{
    OperationResult<User> Create(User user);
    OperationResult<User> Get(Guid id);
}

internal sealed class UserService : IUserService
{
    private readonly IUnitOfWorkFactory _uowFactory;

    public UserService(IUnitOfWorkFactory uowFactory)
    {
        _uowFactory = uowFactory;
    }

    public OperationResult<User> Create(User model)
    {
        if (model.FullName == "Steve") return OperationResult<User>.ErrorResult("Steve cannot be created.");
        if (model.Id != Guid.Empty) return OperationResult<User>.ErrorResult("Id must be empty");
        
        using var uow = _uowFactory.Create();
        var repository = uow.GetRepository<IUserRepository>();

        var entry = repository.Create(Mapper.UserToEntity(model));
        return entry is null 
            ? OperationResult<User>.ErrorResult("Failed to create user.") 
            : OperationResult<User>.SuccessResult(Mapper.UserToModel(entry)!);
    }

    public OperationResult<User> Get(Guid id)
    {
        return OperationResult<User>.FromResult(Constants.Steve);
    }

    public OperationResult<User> Update(User model)
    {
        throw new NotImplementedException();
    }

    public OperationResult<bool> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}