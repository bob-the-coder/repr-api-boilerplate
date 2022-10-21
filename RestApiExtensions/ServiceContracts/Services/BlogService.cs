using DataAccess.Repositories;
using DataAccessExtensions.Contract;
using Domain.Models;
using ServiceContracts.Contract;
using ServiceContracts.Mappers;
using ServiceContracts.Providers;

namespace ServiceContracts.Services;

public interface IBlogService
{
    public OperationResult<Blog> Create(Blog blog);
    public OperationResult<Blog> Get(Guid id);
    public OperationResult<Blog> Update(Blog blog);
}

internal sealed class BlogService : IBlogService
{
    private readonly IDateTimeProvider _dateTime;
    private readonly IUnitOfWorkFactory _uowFactory;

    public BlogService(IDateTimeProvider dateTime, IUnitOfWorkFactory uowFactory)
    {
        _dateTime = dateTime;
        _uowFactory = uowFactory;
    }

    public OperationResult<Blog> Create(Blog blog)
    {
        var utcNow = _dateTime.UtcNow;

        blog.CreatedById = Constants.Steve.Id;
        blog.UpdatedById = Constants.Steve.Id;

        blog.CreatedOnUtc = utcNow;
        blog.UpdatedOnUtc = utcNow;

        using var uow = _uowFactory.Create();
        var repository = uow.GetRepository<IBlogRepository>();

        var created = repository.Create(Mapper.BlogToEntity(blog));
        if (created is null) return OperationResult<Blog>.ErrorResult("Failed to create blog.");

        blog = Mapper.BlogToModel(created);
        return OperationResult<Blog>.SuccessResult(blog);
    }

    public OperationResult<Blog> Get(Guid id)
    {
        using var uow = _uowFactory.Create();
        var repository = uow.GetRepository<IBlogRepository>();

        var found = repository.Get(id);
        if (found is null) return OperationResult<Blog>.ErrorResult("Blog not found.");

        var blog = Mapper.BlogToModel(found);
        return OperationResult<Blog>.SuccessResult(blog);
    }

    public OperationResult<Blog> Update(Blog model)
    {
        using var uow = _uowFactory.Create();
        var repository = uow.GetRepository<IBlogRepository>();

        var found = repository.Get(model.Id);
        if (found is null) return OperationResult<Blog>.ErrorResult("Blog not found.");

        found.Name = model.Name;
        found.Tags = model.Tags;
        found.UpdatedById = Constants.Steve.Id;
        found.UpdatedOnUtc = _dateTime.UtcNow;

        var updated = repository.Update(found);
        if (updated is null) return OperationResult<Blog>.ErrorResult("Failed to update blog.");

        var blog = Mapper.BlogToModel(updated);
        return OperationResult<Blog>.SuccessResult(blog);
    }

    public OperationResult<bool> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}