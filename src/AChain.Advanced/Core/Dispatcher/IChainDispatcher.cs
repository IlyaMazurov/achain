using AChain.Advanced.Core.Processor;
using AChain.Core.Context;
using AChain.Core.Result;

namespace AChain.Advanced.Core.Dispatcher;

public interface IChainDispatcher<TContext>
    where TContext : class, IProcessContext
{
    IProcessorAdvanced<TContext>? GetProcessor(TContext context, IProcessorAdvanced<TContext> currentProcessor);

    IProcessorAdvanced<TContext> GetFirstProcessor(TContext context);
}

public interface IChainDispatcher<TContext, TResult>
    where TContext : class, IProcessContext
    where TResult : class, IProcessResult
{
    IProcessorAdvanced<TContext, TResult>? GetProcessor(TContext context, IProcessorAdvanced<TContext, TResult> currentProcessor);

    IProcessorAdvanced<TContext, TResult> GetFirstProcessor(TContext context);
}
