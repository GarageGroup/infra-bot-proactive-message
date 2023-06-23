using System.Text.Json.Serialization;

namespace GarageGroup.Infra.Bot.Builder;

using NewtonsoftJsonSerializerSettings = Newtonsoft.Json.JsonSerializerSettings;
using NewtonsoftJsonNullValueHandling = Newtonsoft.Json.NullValueHandling;

internal sealed partial class ProactiveMessageJsonConverter : JsonConverter<ProactiveMessageHandlerIn>
{
    private static readonly NewtonsoftJsonSerializerSettings NewtonsoftSerializerSettings;

    static ProactiveMessageJsonConverter()
        =>
        NewtonsoftSerializerSettings = new()
        {
            NullValueHandling = NewtonsoftJsonNullValueHandling.Ignore
        };
}