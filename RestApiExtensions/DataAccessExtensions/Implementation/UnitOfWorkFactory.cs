using DataAccessExtensions.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessExtensions.Implementation;

internal sealed class UnitOfWorkFactory<TDbContext> : IUnitOfWorkFactory where TDbContext : DbContext
{
    private readonly IServiceScopeFactory _scopeFactory;

    public UnitOfWorkFactory(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public IUnitOfWork Create()
    {
        var scope = _scopeFactory.CreateScope();
        
        var dbContext = scope.ServiceProvider.GetService<TDbContext>();
        if (dbContext is null) throw new NullReferenceException();

        return new UnitOfWork<TDbContext>(dbContext, scope);
    }
}