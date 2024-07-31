using AChain.Advanced.Core.Dispatcher;
using AChain.Advanced.Core.Processor;
using AChain.Core.Chain;
using AChain.Core.Context;
using AChain.Core.Result;

namespace AChain.Advanced.Core.Chain;

public interface IChainAdvanced<TContext> : IChainCommon<TContext>
    where TContext : class, IProcessContext
{
    IEnumerable<IProcessorAdvanced<TContext>> GetProcessors();

    IChainDispatcher<TContext> GetDispatcher();

    void SetDispatcherEveryProcessor();
}

public interface IChainAdvanced<TContext, TResult> : IChainCommon<TContext, TResult>
    where TContext : class, IProcessContext
    where TResult : class, IProcessResult
{
    IEnumerable<IProcessorAdvanced<TContext, TResult>> GetProcessors();

    IChainDispatcher<TContext, TResult> GetDispatcher();

    void SetDispatcherEveryProcessor();
}
