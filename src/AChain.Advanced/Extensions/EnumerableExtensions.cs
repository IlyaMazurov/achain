using AChain.Advanced.Core.Processor;
using AChain.Core.Context;
using AChain.Core.Result;

namespace AChain.Advanced.Extensions;

public static class EnumerableExtensions
{
    public static IProcessorAdvanced<TContext> GetProcessorByType<TContext>(this IEnumerable<IProcessorAdvanced<TContext>> processors, Type processorType)
        where TContext : class, IProcessContext
        => processors.First(p => p.GetType() == processorType);

    public static IProcessorAdvanced<TContext, TResult> GetProcessorByType<TContext, TResult>(this IEnumerable<IProcessorAdvanced<TContext, TResult>> processors, Type processorType)
        where TContext : class, IProcessContext
        where TResult : class, IProcessResult
        => processors.First(p => p.GetType() == processorType);

    public static IProcessorAdvanced<TContext> GetProcessorByTypeName<TContext>(this IEnumerable<IProcessorAdvanced<TContext>> processors, string processorTypeName)
        where TContext : class, IProcessContext
        => processors.First(p => p.GetType().Name == processorTypeName);

    public static IProcessorAdvanced<TContext, TResult> GetProcessorByTypeName<TContext, TResult>(this IEnumerable<IProcessorAdvanced<TContext, TResult>> processors, string processorTypeName)
        where TContext : class, IProcessContext
        where TResult : class, IProcessResult
        => processors.First(p => p.GetType().Name == processorTypeName);
}
