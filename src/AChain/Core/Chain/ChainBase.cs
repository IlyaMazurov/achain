using AChain.Core.Context;
using AChain.Core.Processor;
using AChain.Core.Result;
using AChain.Extensions;
using Contextillo;
using Microsoft.Extensions.Logging;

namespace AChain.Core.Chain;

public abstract class ChainBase<TContext> : IChain<TContext>
    where TContext : class, IProcessContext
{
    private IProcessor<TContext>? _chain;
    private readonly ILogger<ChainBase<TContext>>? _logger;

    protected ChainBase(ILogger<ChainBase<TContext>>? logger = null)
    {
        _logger = logger;
    }

    private IProcessor<TContext> Chain => _chain ??= CreateChain();

    protected abstract IEnumerable<IProcessor<TContext>> GetProcessors();

    public virtual IProcessor<TContext> CreateChain()
        => GetProcessors()
            .OrderByNumber()
            .ForEachSetNext()
            .First();

    public virtual void Process(TContext context)
    {
        using (Context<List<string>>.Init(new List<string>()))
        {
            Context<List<string>>.Peek().Add(ToString() ?? "");

            try
            {
                Chain.Process(context);
            }
            finally
            {
                _logger?.LogChain(Context<List<string>>.Peek());
            }
        }
    }

    public virtual async Task ProcessAsync(TContext context)
    {
        using (Context<List<string>>.Init(new List<string>()))
        {
            Context<List<string>>.Peek().Add(ToString() ?? "");

            try
            {
                await Chain.ProcessAsync(context);
            }
            finally
            {
                _logger?.LogChain(Context<List<string>>.Peek());
            }
        }
    }
}

public abstract class ChainBase<TContext, TResult> : IChain<TContext, TResult>
    where TContext : class, IProcessContext
    where TResult : class, IProcessResult
{
    private IProcessor<TContext, TResult>? _chain;
    private readonly ILogger<ChainBase<TContext, TResult>>? _logger;

    protected ChainBase(ILogger<ChainBase<TContext, TResult>>? logger = null)
    {
        _logger = logger;
    }

    private IProcessor<TContext, TResult> Chain => _chain ??= CreateChain();

    protected abstract IEnumerable<IProcessor<TContext, TResult>> GetProcessors();

    public virtual IProcessor<TContext, TResult> CreateChain()
        => GetProcessors()
            .OrderByNumber()
            .ForEachSetNext()
            .First();

    public virtual TResult Process(TContext context)
    {
        using (Context<List<string>>.Init(new List<string>()))
        {
            Context<List<string>>.Peek().Add(ToString() ?? "");

            try
            {
                return Chain.Process(context);
            }
            finally
            {
                _logger?.LogChain(Context<List<string>>.Peek());
            }
        }
    }

    public virtual async Task<TResult> ProcessAsync(TContext context)
    {
        using (Context<List<string>>.Init(new List<string>()))
        {
            Context<List<string>>.Peek().Add(ToString() ?? "");

            try
            {
                return await Chain.ProcessAsync(context);
            }
            finally
            {
                _logger?.LogChain(Context<List<string>>.Peek());
            }
        }
    }
}
