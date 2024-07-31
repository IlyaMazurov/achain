using AChain.Core.Context;
using AChain.Core.Processor;
using AChain.Core.Result;

namespace AChain.Core.Chain;

public interface IChainCommon<in TContext> where TContext : class, IProcessContext
{
    void Process(TContext context);

    Task ProcessAsync(TContext context);
}

public interface IChainCommon<in TContext, TResult>
    where TContext : class, IProcessContext
    where TResult : class, IProcessResult
{
    TResult Process(TContext context);

    Task<TResult> ProcessAsync(TContext context);
}

public interface IChain<TContext> : IChainCommon<TContext> where TContext : class, IProcessContext
{
    IProcessor<TContext> CreateChain();
}

public interface IChain<TContext, TResult> : IChainCommon<TContext, TResult>
    where TContext : class, IProcessContext
    where TResult : class, IProcessResult
{
    IProcessor<TContext, TResult> CreateChain();
}
