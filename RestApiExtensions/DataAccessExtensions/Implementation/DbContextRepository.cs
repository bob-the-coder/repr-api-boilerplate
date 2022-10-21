using DataAccessExtensions.Contract;
using Microsoft.EntityFrameworkCore;

namespace DataAccessExtensions.Implementation;

public class DbContextRepository<TDbContext, TEntity> : IRepository<TEntity>
where TDbContext : DbContext
where TEntity : class, IEntity
{
    protected TDbContext DbContext { get; }

    public DbContextRepository(TDbContext dbContext)
    {
        DbContext = dbContext;
    }
    
    public TEntity? Create(TEntity? entity)
    {
        if (entity is null) return null;

        var entry = DbContext.Set<TEntity>().Add(entity);
        return entry.State == EntityState.Added
            ? entry.Entity
            : null;
    }
    
    public TEntity? Update(TEntity? entity)
    {
        if (entity is null) return null;

        var entry = DbContext.Set<TEntity>().Update(entity);
        return entry.State == EntityState.Modified
            ? entry.Entity
            : null;
    }

    public bool Delete(TEntity? entity)
    {
        if (entity is null) return true;

        var entry = DbContext.Set<TEntity>().Remove(entity);
        return entry.State == EntityState.Deleted;
    }
}