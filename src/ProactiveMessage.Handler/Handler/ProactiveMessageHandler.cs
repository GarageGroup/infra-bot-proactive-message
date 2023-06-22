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
}