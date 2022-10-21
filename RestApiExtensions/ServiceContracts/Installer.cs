using DataAccess;
using DataAccessExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServiceContracts.Providers;
using ServiceContracts.Services;

namespace ServiceContracts;

public static class Installer
{
    public static void AddServiceContracts(this IServiceCollection services)
    {
        services.AddDataAccessExtentions<ExampleContext>(options =>
        {
            options.UseSqlServer("Server=localhost\\SQLEXPRESS;Initial Catalog=ExampleDb;Integrated Security=SSPI;");
        });
        
        services.AddDataAccess();
        
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        
        services.AddScoped<IBlogService, BlogService>();
        services.AddScoped<IUserService, UserService>();
    }
}