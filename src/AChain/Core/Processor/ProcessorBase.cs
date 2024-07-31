using AChain.Core.Context;
using AChain.Core.Result;
using Contextillo;

#pragma warning disable CA1051

namespace AChain.Core.Processor;

public abstract class ProcessorBase<TContext> : IProcessor<TContext> where TContext : class, IProcessContext
{
    private IProcessor<TContext>? _nextProcessor;

    public IProcessor<TContext> SetNext(IProcessor<TContext> processor)
    {
        _nextProcessor = processor;
        return processor;
    }

    public virtual void Process(TContext context)
    {
        Context<List<string>>.Peek().Add(GetType().ToString());

        _nextProcessor?.Process(context);
    }

    public virtual async Task ProcessAsync(TContext context)
    {
        Context<List<string>>.Peek().Add(GetType().ToString());

        if (_nextProcessor is not null)
        {
            await _nextProcessor.ProcessAsync(context);
        }
    }
}

public abstract class ProcessorBase<TContext, TResult> : IProcessor<TContext, TResult>
    where TContext : class, IProcessContext
    where TResult : class, IProcessResult
{
    protected TResult? Result;
    private IProcessor<TContext, TResult>? _nextProcessor;

    public IProcessor<TContext, TResult> SetNext(IProcessor<TContext, TResult> processor)
    {
        _nextProcessor = processor;
        return processor;
    }

    public virtual TResult Process(TContext context)
    {
        Context<List<string>>.Peek().Add(GetType().ToString());

        if (Result is not null)
        {
            return Result;
        }

        if (_nextProcessor is not null)
        {
            return _nextProcessor.Process(context);
        }

        throw new InvalidOperationException($"Incorrectly configured processor or chain. The result is not returned. Processor type: {GetType().Name}");
    }

    public virtual async Task<TResult> ProcessAsync(TContext context)
    {
        Context<List<string>>.Peek().Add(GetType().ToString());

        if (Result is not null)
        {
            return Result;
        }

        if (_nextProcessor is not null)
        {
            return await _nextProcessor.ProcessAsync(context);
        }

        throw new InvalidOperationException($"Incorrectly configured processor or chain. The result is not returned. Processor type: {GetType().Name}");
    }
}
