using System;
using Microsoft.Bot.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PrimeFuncPack;

namespace GarageGroup.Infra.Bot.Builder;

public static class ProactiveMessageHandlerDependency
{
    public static Dependency<IProactiveMessageHandler> UseProactiveMessageHandler<TCloudAdapter>(
        this Dependency<TCloudAdapter, ProactiveMessageHandlerOption> dependency)
        where TCloudAdapter : CloudAdapterBase
    {
        ArgumentNullException.ThrowIfNull(dependency);
        return dependency.Fold<IProactiveMessageHandler>(CreateHandler);

        static ProactiveMessageHandler CreateHandler(
            IServiceProvider serviceProvider, TCloudAdapter cloudAdapter, ProactiveMessageHandlerOption option)
        {
            ArgumentNullException.ThrowIfNull(serviceProvider);
            ArgumentNullException.ThrowIfNull(cloudAdapter);
            ArgumentNullException.ThrowIfNull(option);

            return new(cloudAdapter, option, serviceProvider.ResolveLogger<ProactiveMessageHandler>());
        }
    }

    public static Dependency<IProactiveMessageHandler> UseProactiveMessageHandler<TCloudAdapter>(
        this Dependency<TCloudAdapter> dependency, string botAppIdConfigurationKey = "MicrosoftAppId")
        where TCloudAdapter : CloudAdapterBase
    {
        ArgumentNullException.ThrowIfNull(dependency);
        return dependency.Map<IProactiveMessageHandler>(CreateHandler);

        ProactiveMessageHandler CreateHandler(IServiceProvider serviceProvider, TCloudAdapter cloudAdapter)
        {
            ArgumentNullException.ThrowIfNull(serviceProvider);
            ArgumentNullException.ThrowIfNull(cloudAdapter);

            var configuration = serviceProvider.GetRequiredService<IConfiguration>();

            return new(
                cloudAdapter: cloudAdapter,
                option: new ProactiveMessageHandlerOption(
                    botAppId: configuration[botAppIdConfigurationKey] ?? string.Empty),
                logger: serviceProvider.ResolveLogger<ProactiveMessageHandler>());
        }
    }

    private static ILogger<T>? ResolveLogger<T>(this IServiceProvider serviceProvider)
        =>
        serviceProvider.GetService<ILoggerFactory>()?.CreateLogger<T>();
}