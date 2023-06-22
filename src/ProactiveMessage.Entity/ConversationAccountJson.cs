using System.Text.Json;

namespace GarageGroup.Infra.Bot.Builder;

public sealed record class ConversationAccountJson
{
    public bool? IsGroup { get; init; }

    public string? ConversationType { get; init; }

    public string? Id { get; init; }

    public string? Name { get; init; }

    public string? AadObjectId { get; init; }

    public JsonElement? Properties { get; init; }

    public string? Role { get; init; }

    public string? TenantId { get; init; }
}