using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Agora.Common.EFCore;

/// <summary>
/// Extension method for IServiceCollection to add a DbContext with domain event dispatching capabilities.
/// </summary>
public static class ServiceCollectionExtensions
{    
    /// <summary>
    /// Adds a DbContext of the specified type to the IServiceCollection, along with a DomainEventsSaveChangesInterceptor
    /// to handle domain event dispatching.
    /// </summary>
    /// <typeparam name="TContext">The type of the DbContext to add.</typeparam>
    /// <param name="services">The IServiceCollection to add services to.</param>
    /// <param name="action">An action to configure the DbContextOptionsBuilder.</param>
    /// <returns>The IServiceCollection for chaining.</returns>
    public static IServiceCollection AddDomainEventDbContext<TContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> action)
        where TContext : DbContext
    {
        services.AddDbContext<TContext>((provider, options) =>
        {
            action(options);
            options.AddInterceptors(provider.GetRequiredService<DomainEventsSaveChangesInterceptor>());
        });

        services.AddScoped<DomainEventsSaveChangesInterceptor>();

        return services;
    }
}

