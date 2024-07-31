using AChain.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AChain.Hosting;

public static class HostBuilderExtensions
{
    public static IHostBuilder UseAChain(this IHostBuilder builder) =>
        builder.ConfigureServices((_, collection)
            => collection
                .AddChains()
                .AddProcessors());
}
