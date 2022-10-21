using System.Collections.Concurrent;
using System.Data;
using System.Transactions;
using DataAccessExtensions.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessExtensions.Implementation;

internal sealed class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext> where TDbContext : DbContext
{
    private readonly ConcurrentDictionary<string, IRepository> _cache = new();
    private readonly TDbContext _dbContext;
    private readonly IServiceScope _scope;
    private readonly TransactionScope _transaction;

    public UnitOfWork(TDbContext context, IServiceScope scope)
    {
        _dbContext = context;
        _scope = scope;
        _transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
    }

    public TRepository GetRepository<TRepository>() where TRepository : IRepository
    {
        var key = typeof(TRepository).FullName;

        if (string.IsNullOrWhiteSpace(key)) throw new TypeLoadException();

        if (_cache.ContainsKey(key) && _cache[key] is TRepository fromCache) return fromCache;

        var instance = _scope.ServiceProvider.GetRequiredService<TRepository>();
        if (instance is null) throw new NotImplementedException();

        _cache.AddOrUpdate(key, instance, (oldValue, newValue) => newValue);
        return instance;
    }

    public void Dispose()
    {
        _cache.Clear();
        _dbContext.SaveChanges();
        _transaction.Complete();
        _scope.Dispose();
    }
}