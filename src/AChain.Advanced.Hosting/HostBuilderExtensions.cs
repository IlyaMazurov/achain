using AChain.Advanced.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AChain.Advanced.Hosting;

public static class HostBuilderExtensions
{
    public static IHostBuilder UseAChainAdvanced(this IHostBuilder builder)
        => builder.ConfigureServices((_, collection)
            => collection
                .AddDispatchers()
                .AddChainsAdvanced()
                .AddProcessorsAdvanced());
}
