using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NotesImprovs.DAL.Interfaces;
using NotesImprovs.DAL.Models;
using NotesImprovs.DAL.Repositories;

namespace NotesImprovs.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Retrieve the connection string from configuration
        var connectionString = configuration.GetConnectionString("PostgreSQL")
                               ?? throw new InvalidOperationException("Connection string 'PostgreSQL' not found.");

        // Configure Entity Framework to use SQL Server with the connection string
        services.AddDbContext<NotesImprovsDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddIdentityCore<AppUser>(o =>
            {
                o.User.RequireUniqueEmail = true;
                o.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890 \u0336-._";
            })
            .AddEntityFrameworkStores<NotesImprovsDbContext>()
            .AddDefaultTokenProviders()
            .AddSignInManager<SignInManager<AppUser>>();

        services.AddTransient<INoteRepository, NoteRepository>();
        
        return services;
    }
}