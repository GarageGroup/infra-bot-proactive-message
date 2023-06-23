using Microsoft.Bot.Schema;

namespace GarageGroup.Infra.Bot.Builder;

public sealed record class ProactiveMessageHandlerIn
{
    public Activity? Activity { get; init; }

    public ConversationReference? ConversationReference { get; init; }
}