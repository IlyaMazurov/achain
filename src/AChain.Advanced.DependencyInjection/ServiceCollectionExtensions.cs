using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using AChain.Advanced.Core.Chain;
using AChain.Advanced.Core.Dispatcher;
using AChain.Advanced.Core.Processor;
using AChain.Advanced.Service;
using AChain.Tools;
using Microsoft.Extensions.DependencyInjection;

namespace AChain.Advanced.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddChainsAdvanced(this IServiceCollection serviceCollection)
    {
        TypeTools.ExecuteForEachType(type =>
        {
            var chainAttribute = type.GetCustomAttribute<ChainAdvancedAttribute>();
            if (chainAttribute is null)
            {
                return;
            }

            if (IsDefaultChainType(type))
            {
                serviceCollection.AddScoped(typeof(IChainAdvanced<>), typeof(ChainAdvanced<>));
            }

            if (IsDefaultChainTypeWithResult(type))
            {
                serviceCollection.AddScoped(typeof(IChainAdvanced<,>), typeof(ChainAdvanced<,>));
            }

            if (IsCustomChainType(type))
            {
                var genericTypes = TypeTools.GetGenericTypesByBaseType(type);
                if (genericTypes.Length == 1)
                {
                    serviceCollection.AddScoped(typeof(IChainAdvanced<>).MakeGenericType(genericTypes.First()), type);
                }
            }

            if (IsCustomChainTypeWithResult(type))
            {
                var genericTypes = TypeTools.GetGenericTypesByBaseType(type);
                if (genericTypes.Length == 2)
                {
                    serviceCollection.AddScoped(typeof(IChainAdvanced<,>).MakeGenericType(genericTypes), type);
                }
            }
        });

        return serviceCollection;
    }

    public static IServiceCollection AddProcessorsAdvanced(this IServiceCollection serviceCollection)
    {
        TypeTools.ExecuteForEachType(type =>
        {
            var processorAttribute = type.GetCustomAttribute<ProcessorInChainAdvancedAttribute>();
            if (processorAttribute is null)
            {
                return;
            }

            if (IsProcessorType(type))
            {
                var genericTypes = TypeTools.GetGenericTypesByBaseType(type);
                if (genericTypes.Length == 1)
                {
                    serviceCollection.AddScoped(typeof(IProcessorAdvanced<>).MakeGenericType(genericTypes.First()), type);
                }
            }

            if (IsProcessorTypeWithResult(type))
            {
                var genericTypes = TypeTools.GetGenericTypesByBaseType(type);
                if (genericTypes.Length == 2)
                {
                    serviceCollection.AddScoped(typeof(IProcessorAdvanced<,>).MakeGenericType(genericTypes), type);
                }
            }
        });

        return serviceCollection;
    }

    public static IServiceCollection AddDispatchers(this IServiceCollection serviceCollection)
    {
        TypeTools.ExecuteForEachType(type =>
        {
            var dispatcherAttribute = type.GetCustomAttribute<ChainDispatcherAttribute>();
            if (dispatcherAttribute is null)
            {
                return;
            }

            if (IsDispatcherType(type, out var genericType))
            {
                serviceCollection.AddScoped(typeof(IChainDispatcher<>).MakeGenericType(genericType), type);
            }
            else if (IsDispatcherTypeWithResult(type, out var genericTypes))
            {
                serviceCollection.AddScoped(typeof(IChainDispatcher<,>).MakeGenericType(genericTypes), type);
            }
            else
            {
                throw new InvalidOperationException($"Incorrectly configured dispatcher type: {type.Name}");
            }
        });

        return serviceCollection;
    }

    private static bool IsDefaultChainType(Type type)
        => type == typeof(ChainAdvanced<>);

    private static bool IsDefaultChainTypeWithResult(Type type)
        => type == typeof(ChainAdvanced<,>);

    private static bool IsCustomChainType(Type type)
    {
        var genericTypes = TypeTools.GetGenericTypesByBaseType(type);
        return genericTypes.Length == 1 &&
               type != typeof(ChainAdvanced<>) &&
               typeof(IChainAdvanced<>).MakeGenericType(genericTypes.First()).IsAssignableFrom(type);
    }

    private static bool IsCustomChainTypeWithResult(Type type)
    {
        var genericTypes = TypeTools.GetGenericTypesByBaseType(type);
        return genericTypes.Length == 2 &&
               type != typeof(ChainAdvanced<,>) &&
               typeof(IChainAdvanced<,>).MakeGenericType(genericTypes).IsAssignableFrom(type);
    }

    private static bool IsProcessorType(Type type)
    {
        var genericTypes = TypeTools.GetGenericTypesByBaseType(type);
        return genericTypes.Length == 1 &&
               typeof(IProcessorAdvanced<>).MakeGenericType(genericTypes.First()).IsAssignableFrom(type);
    }

    private static bool IsProcessorTypeWithResult(Type type)
    {
        var genericTypes = TypeTools.GetGenericTypesByBaseType(type);
        return genericTypes.Length == 2 &&
               typeof(IProcessorAdvanced<,>).MakeGenericType(genericTypes).IsAssignableFrom(type);
    }

    private static bool IsDispatcherType(Type type, [NotNullWhen(true)] out Type? genericType)
    {
        var baseInterface = type
            .GetInterfaces()
            .FirstOrDefault(i =>
                i.GenericTypeArguments.Length == typeof(IChainDispatcher<>).GetGenericArguments().Length &&
                i == typeof(IChainDispatcher<>).MakeGenericType(i.GenericTypeArguments));

        if (baseInterface is not null)
        {
            genericType = baseInterface.GenericTypeArguments.First();
            return true;
        }

        genericType = null;
        return false;
    }

    private static bool IsDispatcherTypeWithResult(Type type, [NotNullWhen(true)] out Type[]? genericTypes)
    {
        var baseInterface = type
            .GetInterfaces()
            .FirstOrDefault(i =>
                i.GenericTypeArguments.Length == typeof(IChainDispatcher<,>).GetGenericArguments().Length &&
                i == typeof(IChainDispatcher<,>).MakeGenericType(i.GenericTypeArguments));

        if (baseInterface is not null)
        {
            genericTypes = baseInterface.GenericTypeArguments;
            return true;
        }

        genericTypes = null;
        return false;
    }
}
