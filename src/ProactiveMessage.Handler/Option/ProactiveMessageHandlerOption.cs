namespace GarageGroup.Infra.Bot.Builder;

public sealed record class ProactiveMessageHandlerOption
{
    public ProactiveMessageHandlerOption(string botAppId)
        =>
        BotAppId = botAppId ?? string.Empty;

    public string BotAppId { get; }
}