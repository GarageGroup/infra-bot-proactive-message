using System;

namespace GarageGroup.Infra.Bot.Builder;

public interface IProactiveMessageHandler : IHandler<ProactiveMessageHandlerIn, Unit>
{
}