using System.Reflection;
using AChain.Core.Chain;
using AChain.Core.Processor;
using AChain.Service;
using AChain.Tools;
using Microsoft.Extensions.DependencyInjection;

namespace AChain.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddChains(this IServiceCollection serviceCollection)
    {
        TypeTools.ExecuteForEachType(type =>
        {
            var chainAttribute = type.GetCustomAttribute<ChainAttribute>();
            if (chainAttribute is null)
            {
                return;
            }

            if (IsDefaultChainType(type))
            {
                serviceCollection.AddScoped(typeof(IChain<>), typeof(Chain<>));
            }
            else if (IsDefaultChainTypeWithResult(type))
            {
                serviceCollection.AddScoped(typeof(IChain<,>), typeof(Chain<,>));
            }
            else if (IsCustomChainType(type))
            {
                var genericTypes = TypeTools.GetGenericTypesByBaseType(type);
                if (genericTypes.Length == 1)
                {
                    serviceCollection.AddScoped(typeof(IChain<>).MakeGenericType(genericTypes.First()), type);
                }
            }
            else if (IsCustomChainTypeWithResult(type))
            {
                var genericTypes = TypeTools.GetGenericTypesByBaseType(type);
                if (genericTypes.Length == 2)
                {
                    serviceCollection.AddScoped(typeof(IChain<,>).MakeGenericType(genericTypes), type);
                }
            }
        });

        return serviceCollection;
    }

    public static IServiceCollection AddProcessors(this IServiceCollection serviceCollection)
    {
        TypeTools.ExecuteForEachType(type =>
        {
            var processorAttribute = type.GetCustomAttribute<ProcessorInChainAttribute>();
            if (processorAttribute is null)
            {
                return;
            }

            if (IsProcessorType(type))
            {
                var genericTypes = TypeTools.GetGenericTypesByBaseType(type);
                if (genericTypes.Length == 1)
                {
                    serviceCollection.AddScoped(typeof(IProcessor<>).MakeGenericType(genericTypes.First()), type);
                }
            }

            if (IsProcessorTypeWithResult(type))
            {
                var genericTypes = TypeTools.GetGenericTypesByBaseType(type);
                if (genericTypes.Length == 2)
                {
                    serviceCollection.AddScoped(typeof(IProcessor<,>).MakeGenericType(genericTypes), type);
                }
            }
        });

        return serviceCollection;
    }

    private static bool IsDefaultChainType(Type type)
        => type == typeof(Chain<>);

    private static bool IsDefaultChainTypeWithResult(Type type)
        => type == typeof(Chain<,>);

    private static bool IsProcessorType(Type type)
    {
        var genericTypes = TypeTools.GetGenericTypesByBaseType(type);
        return genericTypes.Length == 1 &&
               typeof(IProcessor<>).MakeGenericType(genericTypes.First()).IsAssignableFrom(type);
    }

    private static bool IsProcessorTypeWithResult(Type type)
    {
        var genericTypes = TypeTools.GetGenericTypesByBaseType(type);
        return genericTypes.Length == 2 &&
               typeof(IProcessor<,>).MakeGenericType(genericTypes).IsAssignableFrom(type);
    }

    private static bool IsCustomChainType(Type type)
    {
        var genericTypes = TypeTools.GetGenericTypesByBaseType(type);
        return genericTypes.Length == 1 &&
               type != typeof(Chain<>) &&
               typeof(IChain<>).MakeGenericType(genericTypes.First()).IsAssignableFrom(type);
    }

    private static bool IsCustomChainTypeWithResult(Type type)
    {
        var genericTypes = TypeTools.GetGenericTypesByBaseType(type);
        return genericTypes.Length == 2 &&
               type != typeof(Chain<,>) &&
               typeof(IChain<,>).MakeGenericType(genericTypes).IsAssignableFrom(type);
    }
}
