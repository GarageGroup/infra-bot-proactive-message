using System;
using System.Text.Json;

namespace GarageGroup.Infra.Bot.Builder;

public sealed record class ConversationReferenceJson
{
    private static readonly JsonSerializerOptions SerializerOptions;

    static ConversationReferenceJson()
        =>
        SerializerOptions = new(JsonSerializerDefaults.Web);

    public static ConversationReferenceJson DeserializeFromJson(string json)
    {
        return JsonSerializer.Deserialize<ConversationReferenceJson>(json, SerializerOptions) ?? throw CreateNullDeserializationResultException(json);

        static InvalidOperationException CreateNullDeserializationResultException(string conversationReferenceJson)
            =>
            new($"Deserialization result of {nameof(ConversationReferenceJson)} is null. Source value: {conversationReferenceJson}");
    }

    public string? ActivityId { get; init; }

    public ChannelAccountJson? User { get; init; }

    public ChannelAccountJson? Bot { get; init; }

    public ConversationAccountJson? Conversation { get; init; }

    public string? ChannelId { get; init; }

    public string? Locale { get; init; }

    public string? ServiceUrl { get; init; }
}