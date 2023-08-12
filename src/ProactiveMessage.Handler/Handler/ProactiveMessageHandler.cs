using System.Net;
using Microsoft.Bot.Builder;

namespace GarageGroup.Infra.Bot.Builder;

internal sealed partial class ProactiveMessageHandler : IProactiveMessageHandler
{
    private readonly CloudAdapterBase cloudAdapter;

    private readonly ProactiveMessageHandlerOption option;

    internal ProactiveMessageHandler(CloudAdapterBase cloudAdapter, ProactiveMessageHandlerOption option)
    {
        this.cloudAdapter = cloudAdapter;
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