using AChain.Core.Context;
using AChain.Core.Result;

namespace AChain.Core.Processor;

public interface IProcessorCommon<in TContext>
    where TContext : class, IProcessContext
{
    void Process(TContext context);

    Task ProcessAsync(TContext context);
}

public interface IProcessorCommon<in TContext, TResult>
    where TContext : class, IProcessContext
    where TResult : class, IProcessResult
{
    TResult Process(TContext context);

    Task<TResult> ProcessAsync(TContext context);
}

public interface IProcessor<TContext> : IProcessorCommon<TContext>
    where TContext : class, IProcessContext
{
    IProcessor<TContext> SetNext(IProcessor<TContext> processor);
}

public interface IProcessor<TContext, TResult> : IProcessorCommon<TContext, TResult>
    where TContext : class, IProcessContext
    where TResult : class, IProcessResult
{
    IProcessor<TContext, TResult> SetNext(IProcessor<TContext, TResult> processor);
}
