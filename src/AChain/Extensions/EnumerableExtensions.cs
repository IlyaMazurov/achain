using System.Reflection;
using AChain.Core.Context;
using AChain.Core.Processor;
using AChain.Core.Result;

namespace AChain.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T> OrderByNumber<T>(this IEnumerable<T> enumerable)
    {
        return enumerable.OrderBy(p =>
        {
            var processorAttribute = (p?.GetType().GetCustomAttribute<ProcessorInChainAttribute>()) ?? throw new InvalidOperationException($"Incorrectly configured processor type: {p?.GetType().Name}");
            return processorAttribute.Number;
        });
    }

    public static IEnumerable<IProcessor<TContext>> ForEachSetNext<TContext>(this IEnumerable<IProcessor<TContext>> processors)
        where TContext : class, IProcessContext
    {
        var array = processors.ToArray();

        for (var index = 0; index < array.Length - 1; ++index)
        {
            array[index].SetNext(array[index + 1]);
        }

        return array;
    }

    public static IEnumerable<IProcessor<TContext, TResult>> ForEachSetNext<TContext, TResult>(this IEnumerable<IProcessor<TContext, TResult>> processors)
        where TContext : class, IProcessContext
        where TResult : class, IProcessResult
    {
        var array = processors.ToArray();

        for (var index = 0; index < array.Length - 1; ++index)
        {
            array[index].SetNext(array[index + 1]);
        }

        return array;
    }
}
