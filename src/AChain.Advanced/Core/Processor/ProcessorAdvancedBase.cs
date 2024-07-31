using AChain.Advanced.Core.Dispatcher;
using AChain.Core.Context;
using AChain.Core.Result;
using Contextillo;

#pragma warning disable CA1051
#pragma warning disable CA1716

namespace AChain.Advanced.Core.Processor;

public abstract class ProcessorAdvancedBase<TContext> : IProcessorAdvanced<TContext> where TContext : class, IProcessContext
{
    private IChainDispatcher<TContext>? _dispatcher;
    private IChainDispatcher<TContext> Dispatcher
        => _dispatcher ?? throw new InvalidOperationException($"Incorrectly configured advanced processor type: {GetType().Name}, dispatcher not exist");

    public void SetDispatcher(IChainDispatcher<TContext> dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public virtual void Process(TContext context)
    {
        Context<List<string>>.Peek().Add(GetType().ToString());
        Next(context);
    }

    public virtual async Task ProcessAsync(TContext context)
    {
        Context<List<string>>.Peek().Add(GetType().ToString());
        await NextAsync(context);
    }

    protected virtual void Next(TContext context)
        => Dispatcher.GetProcessor(context, this)?.Process(context);

    protected virtual async Task NextAsync(TContext context)
    {
        var processor = Dispatcher.GetProcessor(context, this);
        if (processor is not null)
        {
            await processor.ProcessAsync(context);
        }
    }
}

public abstract class ProcessorAdvancedBase<TContext, TResult> : IProcessorAdvanced<TContext, TResult>
    where TContext : class, IProcessContext
    where TResult : class, IProcessResult
{
    protected TResult? Result;

    private IChainDispatcher<TContext, TResult>? _dispatcher;
    private IChainDispatcher<TContext, TResult> Dispatcher
        => _dispatcher ?? throw new InvalidOperationException($"Incorrectly configured advanced processor type: {GetType().Name}, dispatcher not exist");

    public void SetDispatcher(IChainDispatcher<TContext, TResult> dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public virtual TResult Process(TContext context)
        => Next(context);

    public virtual async Task<TResult> ProcessAsync(TContext context)
        => await NextAsync(context);

    protected virtual TResult Next(TContext context)
    {
        Context<List<string>>.Peek().Add(GetType().ToString());

        if (Result is not null)
        {
            return Result;
        }

        var processor = Dispatcher.GetProcessor(context, this);
        if (processor is not null)
        {
            return processor.Process(context);
        }

        throw new InvalidOperationException($"Incorrectly configured advanced processor or chain. The result is not returned. Processor type: {GetType().Name}");
    }

    protected virtual async Task<TResult> NextAsync(TContext context)
    {
        Context<List<string>>.Peek().Add(GetType().ToString());

        if (Result is not null)
        {
            return Result;
        }

        var processor = Dispatcher.GetProcessor(context, this);
        if (processor is not null)
        {
            return await processor.ProcessAsync(context);
        }

        throw new InvalidOperationException($"Incorrectly configured advanced processor or chain. The result is not returned. Processor type: {GetType().Name}");
    }
}
