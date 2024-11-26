using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotesImprovs.BLL.Interfaces;
using NotesImprovs.BLL.Managers;
using NotesImprovs.BLL.Services;

namespace NotesImprovs.BLL;

public static class DependencyInjection
{
    public static IServiceCollection AddBllServices(this IServiceCollection services)
    {
        services.AddTransient<INotesManager, NotesManager>();
        services.AddScoped<TokenService>();
        services.AddScoped<AuthRedisService>();
        
        return services;
    }
}