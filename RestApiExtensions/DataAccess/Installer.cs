using DataAccess.Entities;
using DataAccess.Repositories;
using DataAccessExtensions.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess;

public static class Installer
{
    public static void AddDataAccess(this IServiceCollection services)
    {
        services.AddTransient<IRepository<User>, UserRepository>();
        services.AddTransient<IRepository<Blog>, BlogRepository>();
        services.AddTransient<IRepository<BlogPost>, BlogPostRepository>();
    }
}