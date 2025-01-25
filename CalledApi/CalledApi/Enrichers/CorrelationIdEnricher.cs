using Serilog.Core;
using Serilog.Events;

namespace CalledApi.Enrichers;

public class CorrelationIdEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var correlationId = Guid.NewGuid(); // or retrieve the correlation ID from somewhere else
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("CorrelationId", correlationId));
    }
}