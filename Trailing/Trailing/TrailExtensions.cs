using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Trailing;

public static class TrailExtensions
{
    public static IServiceCollection SetupTrail(this IServiceCollection services, IConfiguration configuration) => services
        .AddOthers()
        .AddDelegatingHandlers()
        .AddDataAccess()
        .AddServices()
        .AddOptions(configuration);


    private static IServiceCollection AddOthers(this IServiceCollection services) => services
        .AddHttpContextAccessor()
        .AddLogging();

    private static IServiceCollection AddServices(this IServiceCollection services) => services
        .AddScoped<ITrailService, TrailService>();

    private static IServiceCollection AddDataAccess(this IServiceCollection services) => services
        .AddScoped<ITrailDataAccess, TrailDataAccess>();

    private static IServiceCollection AddDelegatingHandlers(this IServiceCollection services) => services
        .AddTransient<TrailDelegatingHandler>();

    private static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration) => services
        .Configure<TrailOptions>(options => configuration.GetSection("Trail").Bind(options));
}
