using Microsoft.Extensions.DependencyInjection;

namespace CalledApi.Trailing;

public static class TrailExtensions
{
    public static IServiceCollection SetupTrail(this IServiceCollection services) => services
        .AddScoped<ITrailService, TrailService>()
        .AddScoped<ITrailDataAccess, TrailDataAccess>();

}
