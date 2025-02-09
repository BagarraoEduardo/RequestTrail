using System;
using System.Reflection;
using CallerApi.Business.Interfaces;
using CallerApi.Business.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace CallerApi.Business.Extensions;

public static class BusinessExtensions
{
    public static IServiceCollection SetupBusiness(this IServiceCollection services) => services
        .AddOthers()
        .AddServices();

    public static IServiceCollection AddOthers(this IServiceCollection services) => services
        .AddAutoMapper(typeof(BusinessMapper));

    public static IServiceCollection AddServices(this IServiceCollection services) => services
        .AddScoped<IRequestService, RequestService>();
}
