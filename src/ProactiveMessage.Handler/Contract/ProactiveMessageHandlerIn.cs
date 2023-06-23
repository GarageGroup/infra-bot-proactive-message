using System.Text.Json.Serialization;
using Microsoft.Bot.Schema;

namespace GarageGroup.Infra.Bot.Builder;

[JsonConverter(typeof(ProactiveMessageJsonConverter))]
public sealed record class ProactiveMessageHandlerIn
{
    public Activity? Activity { get; init; }

    public ConversationReference? ConversationReference { get; init; }
}