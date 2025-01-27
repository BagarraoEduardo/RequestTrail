using Microsoft.Extensions.DependencyInjection;

namespace CalledApi.Trailing;

public static class TrailExtensions
{
    public static IServiceCollection SetupTrail(this IServiceCollection services) => services
        .AddOthers()
        .AddDataAccess()
        .AddServices();


    public static IServiceCollection AddOthers(this IServiceCollection services) => services
        .AddLogging();

    public static IServiceCollection AddServices(this IServiceCollection services) => services
        .AddScoped<ITrailService, TrailService>();

    public static IServiceCollection AddDataAccess(this IServiceCollection services) => services
        .AddScoped<ITrailDataAccess, TrailDataAccess>();

}
