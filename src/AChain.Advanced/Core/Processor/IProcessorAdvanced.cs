using AChain.Advanced.Core.Dispatcher;
using AChain.Core.Context;
using AChain.Core.Processor;
using AChain.Core.Result;

namespace AChain.Advanced.Core.Processor;

public interface IProcessorAdvanced<TContext> : IProcessorCommon<TContext>
    where TContext : class, IProcessContext
{
    void SetDispatcher(IChainDispatcher<TContext> dispatcher);
}

public interface IProcessorAdvanced<TContext, TResult> : IProcessorCommon<TContext, TResult>
    where TContext : class, IProcessContext
    where TResult : class, IProcessResult
{
    void SetDispatcher(IChainDispatcher<TContext, TResult> dispatcher);
}
