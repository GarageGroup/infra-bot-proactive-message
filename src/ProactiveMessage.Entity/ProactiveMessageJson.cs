namespace GarageGroup.Infra.Bot.Builder;

public sealed record class ProactiveMessageJson
{
    public ProactiveMessageJson(ActivityJson activity, ConversationReferenceJson conversationReference)
    {
        Activity = activity;
        ConversationReference = conversationReference;
    }

    public ActivityJson Activity { get; }

    public ConversationReferenceJson ConversationReference { get; }
}