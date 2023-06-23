using System.Text.Json;

namespace GarageGroup.Infra.Bot.Builder;

public sealed record class ChannelAccountJson
{
    public string? Id { get; init; }

    public string? Name { get; init; }

    public string? AadObjectId { get; init; }

    public JsonElement? Properties { get; init; }

    public string? Role { get; init; }
}