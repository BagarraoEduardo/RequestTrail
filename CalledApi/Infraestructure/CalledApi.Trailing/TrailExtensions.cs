// using Microsoft.Extensions.DependencyInjection;

// namespace CalledApi.Trailing;

// public static class TrailExtensions
// {
//     public static IServiceCollection SetupTrail(this IServiceCollection services) => services
//         .AddOthers()
//         .AddDelegatingHandlers()
//         .AddDataAccess()
//         .AddServices();


//     public static IServiceCollection AddOthers(this IServiceCollection services) => services
//         .AddHttpContextAccessor()
//         .AddLogging();

//     public static IServiceCollection AddServices(this IServiceCollection services) => services
//         .AddScoped<ITrailService, TrailService>();

//     public static IServiceCollection AddDataAccess(this IServiceCollection services) => services
//         .AddScoped<ITrailDataAccess, TrailDataAccess>();

//     public static IServiceCollection AddDelegatingHandlers(this IServiceCollection services) => services
//         .AddTransient<TrailDelegatingHandler>();
// }
