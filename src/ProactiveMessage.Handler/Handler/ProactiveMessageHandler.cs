using System.Net;
using Microsoft.Bot.Builder;
using Microsoft.Extensions.Logging;

namespace GarageGroup.Infra.Bot.Builder;

internal sealed partial class ProactiveMessageHandler : IProactiveMessageHandler
{
    private readonly CloudAdapterBase cloudAdapter;

    private readonly ProactiveMessageHandlerOption option;

    private readonly ILogger? logger;

    internal ProactiveMessageHandler(CloudAdapterBase cloudAdapter, ProactiveMessageHandlerOption option, ILogger<ProactiveMessageHandler>? logger)
    {
        this.cloudAdapter = cloudAdapter;
        this.logger = logger;
        this.option = option;
    }

    private static bool IsTransientErrorCode(HttpStatusCode statusCode)
    {
        if (statusCode is HttpStatusCode.RequestTimeout or HttpStatusCode.TooManyRequests)
        {
            return true;
        }

        return (int)statusCode >= 500;
    }
}