using Serilog;
using Serilog.Sinks.MariaDB;
using Serilog.Sinks.MariaDB.Extensions;

namespace CalledApi.Extensions;

public static class PresentationExtensions
{
    public static IServiceCollection SetupPresentation(this IServiceCollection services, IConfiguration configuration, IHostBuilder host)
    {
        services.AddLogging(configuration, host);

        return services;
    }

    private static void AddLogging(this IServiceCollection services,  IConfiguration configuration, IHostBuilder host)
    {
        Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine(msg));

        var options = new MariaDBSinkOptions();

        options.PropertiesToColumnsMapping.Add("CorrelationId", "CorrelationId");

        Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .WriteTo.MariaDB(
            connectionString: configuration.GetConnectionString("DefaultConnection"),
            autoCreateTable: true,
            tableName: "Log",
            options: options)
        .Enrich.FromLogContext()
        .CreateLogger();

        host.UseSerilog();
    }
}