using Backend.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Backend;

public static class ServiceCollectionExtension
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Database");
        if (connectionString is null)
            throw new ArgumentNullException("connection string is empty");
        services.AddDbContext<ApplicationContext>(option =>
        {
            option.UseNpgsql(connectionString).EnableSensitiveDataLogging();
        });
    }
}
