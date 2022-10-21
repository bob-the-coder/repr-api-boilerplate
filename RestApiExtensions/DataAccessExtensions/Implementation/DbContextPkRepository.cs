using DataAccessExtensions.Contract;
using Microsoft.EntityFrameworkCore;

namespace DataAccessExtensions.Implementation;

public class DbContextPkRepository<TDbContext, TEntity, TPk> : DbContextRepository<TDbContext, TEntity>, IPkRepository<TEntity, TPk>
    where TDbContext : DbContext
    where TEntity : class, IPkEntity<TPk>
    where TPk : IComparable
{
    public DbContextPkRepository(TDbContext dbContext) : base(dbContext)
    {
    }
    
    public TEntity? Get(TPk pk)
    {
        return DbContext.Set<TEntity>().FirstOrDefault(entity => entity.Id.Equals(pk));
    }

    public bool Delete(TPk key)
    {
        var entity = Get(key);
        if (entity is null) return true;

        var entry = DbContext.Set<TEntity>().Remove(entity);
        return entry.State == EntityState.Deleted;
    }
}