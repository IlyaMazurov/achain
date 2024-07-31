using AChain.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

#pragma warning disable CA1716

namespace AChain.Test.Default.Configuration;

public static class ServiceProviderConfigurator
{
    public static IServiceProvider Configure()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddChains();
        serviceCollection.AddProcessors();
        serviceCollection.AddLogging(builder => builder.AddConsole());

        return serviceCollection.BuildServiceProvider();
    }
}
