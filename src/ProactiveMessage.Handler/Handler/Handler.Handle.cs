using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;

namespace GarageGroup.Infra.Bot.Builder;

partial class ProactiveMessageHandler
{
    public ValueTask<Result<Unit, Failure<HandlerFailureCode>>> HandleAsync(
        ProactiveMessageHandlerIn? input, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return ValueTask.FromCanceled<Result<Unit, Failure<HandlerFailureCode>>>(cancellationToken);
        }

        return InnerHandleAsync(input, cancellationToken);
    }

    private async ValueTask<Result<Unit, Failure<HandlerFailureCode>>> InnerHandleAsync(
        ProactiveMessageHandlerIn? messageJson, CancellationToken cancellationToken)
    {
        if (messageJson?.Activity is null)
        {
            return Failure.Create(HandlerFailureCode.Persistent, "Activity must be specified");
        }

        if (messageJson.ConversationReference is null)
        {
            return Failure.Create(HandlerFailureCode.Persistent, "ConversationReference must be specified");
        }

        var botAppId = option.BotAppId;
        var conversation = messageJson.ConversationReference;

        await cloudAdapter.ContinueConversationAsync(botAppId, conversation, SendAsync, cancellationToken).ConfigureAwait(false);
        return Result.Success<Unit>(default);

        Task SendAsync(ITurnContext turnContext, CancellationToken cancellationToken)
            =>
            turnContext.SendActivityAsync(messageJson.Activity, cancellationToken);
    }
}