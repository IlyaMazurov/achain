using AChain.Advanced.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AChain.Test.Advanced.Configuration;

public static class ServiceProviderConfigurator
{
    public static IServiceProvider Configure()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection
            .AddDispatchers()
            .AddChainsAdvanced()
            .AddProcessorsAdvanced();

        serviceCollection.AddLogging(builder => builder.AddConsole());

        return serviceCollection.BuildServiceProvider();
    }
}
