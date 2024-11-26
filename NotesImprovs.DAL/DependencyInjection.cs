using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotesImprovs.DAL.Models;

namespace NotesImprovs.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Retrieve the connection string from configuration
        var connectionString = configuration.GetConnectionString("ConnectionString")
                               ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        // Configure Entity Framework to use SQL Server with the connection string
        services.AddDbContext<NotesImprovsDbContext>(options =>
            options.UseNpgsql(connectionString));
        
        services.AddIdentityCore<AppUser>()
            .AddEntityFrameworkStores<NotesImprovsDbContext>();

        return services;
    }
}