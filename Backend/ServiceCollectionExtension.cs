using System.Reflection;
using Backend.Shared.Auth;
using Backend.Shared.Domain;
using Backend.Shared.Interfaces;
using Backend.Shared.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend;

public static class ServiceCollectionExtension
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        services.AddDependencies();
    }

    private static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Database");
        if (connectionString is null)
            throw new ArgumentNullException("connection string is empty");
        services.AddDbContext<ApplicationContext>(option =>
        {
            option.UseNpgsql(connectionString).EnableSensitiveDataLogging();
        });
    }

    private static void AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<AuthService>();
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
    }


    public static void RegisterEndpoints(this IEndpointRouteBuilder app)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var iEndpoint = typeof(IEndpoint);
        var endpoints = assembly.GetTypes()
        .Where(e => e is { IsClass: true, IsAbstract: false } && iEndpoint.IsAssignableFrom(e));
        foreach (var endpoint in endpoints)
        {
            var instance = Activator.CreateInstance(endpoint) as IEndpoint;
            instance?.Register(app);
        }

    }
}
