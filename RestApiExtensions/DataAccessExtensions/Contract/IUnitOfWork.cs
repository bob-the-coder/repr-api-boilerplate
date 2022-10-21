using Microsoft.EntityFrameworkCore;

namespace DataAccessExtensions.Contract;

public interface IUnitOfWork : IDisposable
{
    TRepository GetRepository<TRepository>() where TRepository : IRepository;
}

public interface IUnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
{
}