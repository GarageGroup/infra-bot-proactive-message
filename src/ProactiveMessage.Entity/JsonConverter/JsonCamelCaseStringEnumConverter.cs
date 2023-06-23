using System.Text.Json;
using System.Text.Json.Serialization;

namespace GarageGroup.Infra.Bot.Builder;

internal sealed class JsonCamelCaseStringEnumConverter : JsonStringEnumConverter
{
    public JsonCamelCaseStringEnumConverter() : base(JsonNamingPolicy.CamelCase)
    {
    }
}