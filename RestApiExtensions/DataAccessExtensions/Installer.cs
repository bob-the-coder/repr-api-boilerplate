using DataAccessExtensions.Contract;
using DataAccessExtensions.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessExtensions;

public static class Installer
{
    public static void AddDataAccessExtentions<TDbContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> config) where TDbContext : DbContext
    {
        services.AddDbContextFactory<TDbContext>(config);
        services.AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory<TDbContext>>();
    }
}