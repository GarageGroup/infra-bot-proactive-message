using System.Diagnostics;
using System.Text.Json;

namespace GarageGroup.Infra.Bot.Builder;

using NewtonsoftJsonConvert = Newtonsoft.Json.JsonConvert;

partial class ProactiveMessageJsonConverter
{
    public override void Write(Utf8JsonWriter writer, ProactiveMessageHandlerIn value, JsonSerializerOptions options)
    {
        Debug.Assert(writer is not null);
        Debug.Assert(options is not null);

        var jsonString = NewtonsoftJsonConvert.SerializeObject(value, NewtonsoftSerializerSettings);

        using var jsonDoc = JsonDocument.Parse(jsonString);
        jsonDoc.WriteTo(writer);
    }
}