using System;
using System.IO.Compression;
using CallerApi.Integration.Interfaces;
using CallerApi.Integration.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Trailing;
using CallerApi.Integration.Clients.CalledApi;

namespace CallerApi.Integration.Extensions;

public static class IntegrationExtensions
{
    public static IServiceCollection SetupIntegration(this IServiceCollection services, IConfiguration configuration) => services
        .AddOptions(configuration)
        .AddHttpClients(configuration);

    private static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration) => services
        .Configure<CalledApiOptions>(options => configuration.GetSection("CalledApi").Bind(options));


private static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration) => services
    .AddHttpClient<IMainClient, MainClient>(client =>
    {
        var url = configuration["CalledApi:Url"];
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new InvalidOperationException("CalledApi:Url configuration key is missing or empty.");
        }
        client.BaseAddress = new Uri(url);
    })
    .AddHttpMessageHandler<TrailDelegatingHandler>()
    .Services;
}
