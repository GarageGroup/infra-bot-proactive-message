using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;

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

        return await ContinueConversationAsync(botAppId, conversation, SendAsync, cancellationToken).ConfigureAwait(false);

        Task SendAsync(ITurnContext turnContext, CancellationToken cancellationToken)
            =>
            turnContext.SendActivityAsync(messageJson.Activity, cancellationToken);
    }

    private async Task<Result<Unit, Failure<HandlerFailureCode>>> ContinueConversationAsync(
        string botAppId, ConversationReference reference, BotCallbackHandler callback, CancellationToken cancellationToken)
    {
        try
        {
            await cloudAdapter.ContinueConversationAsync(botAppId, reference, callback, cancellationToken).ConfigureAwait(false);
            return Result.Success<Unit>(default);
        }
        catch (ErrorResponseException exception) when (IsTransientErrorCode(exception.Response.StatusCode) is false)
        {
            logger?.LogError(exception, "An unexpected persistent error response occured when trying to send a message to a bot");
            return Failure.Create(HandlerFailureCode.Persistent, exception.Body.Error.Message);
        }
    }
}