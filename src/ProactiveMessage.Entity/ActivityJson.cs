using System;
using System.Text.Json;

namespace GarageGroup.Infra.Bot.Builder;

public sealed record class ActivityJson
{
    private static readonly JsonSerializerOptions SerializerOptions;

    static ActivityJson()
        =>
        SerializerOptions = new(JsonSerializerDefaults.Web);

    public static ActivityJson DeserializeFromJson(string json)
    {
        return JsonSerializer.Deserialize<ActivityJson>(json, SerializerOptions) ?? throw CreateNullDeserializationResultException(json);

        static InvalidOperationException CreateNullDeserializationResultException(string conversationReferenceJson)
            =>
            new($"Deserialization result of {nameof(ActivityJson)} is null. Source value: {conversationReferenceJson}");
    }

    public ActivityJson(ActivityType type = ActivityType.Message)
        =>
        Type = type;

    public ActivityType Type { get; init; }

    public string? Text { get; init; }

    public JsonElement? ChannelData { get; init; }
}