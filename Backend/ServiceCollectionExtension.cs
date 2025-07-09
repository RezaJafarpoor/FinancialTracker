using System.Reflection;
using System.Text;
using Backend.Shared.Auth;
using Backend.Shared.Domain;
using Backend.Shared.Interfaces;
using Backend.Shared.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing.Tree;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Backend;

public static class ServiceCollectionExtension
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        services.AddDependencies();
        services.AddIdentity(configuration);
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
        services.AddScoped<TokenProvider>();
    }


    private static void AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<JwtSetting>()
        .Bind(config: configuration.GetSection("JwtSetting"));
        services.AddAuthorization();
        services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(option =>
        {
            option.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var token = context.Request.Cookies["access_token"];
                    context.Token = token;
                    return Task.CompletedTask;
                }

            };
            var jwtOption = configuration.GetSection("JwtSetting").Get<JwtSetting>();
            if (jwtOption is null)
                throw new Exception("Something is wrong with the jwt setting");
            option.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                RequireExpirationTime = true,
                ClockSkew = TimeSpan.FromMinutes(1),
                ValidIssuer = jwtOption.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtOption.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.Secret))
            };
        });
    }


}
