using System;
using System.Diagnostics;
using System.Text.Json;

namespace GarageGroup.Infra.Bot.Builder;

using NewtonsoftJsonConvert = Newtonsoft.Json.JsonConvert;

partial class ProactiveMessageJsonConverter
{
    public override ProactiveMessageHandlerIn? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        Debug.Assert(reader.TokenType is not JsonTokenType.None);
        Debug.Assert(options is not null);

        if (reader.TokenType is JsonTokenType.Null)
        {
            return default;
        }

        if (reader.TokenType is not JsonTokenType.StartObject)
        {
            throw new JsonException("The last processed JSON token is not the start of a JSON object.");
        }

        using var jsonDoc = JsonDocument.ParseValue(ref reader);
        var jsonString = jsonDoc.RootElement.GetRawText();

        return NewtonsoftJsonConvert.DeserializeObject<ProactiveMessageHandlerIn>(jsonString, NewtonsoftSerializerSettings);
    }
}