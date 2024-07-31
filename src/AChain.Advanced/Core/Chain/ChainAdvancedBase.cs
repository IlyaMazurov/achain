using AChain.Advanced.Core.Dispatcher;
using AChain.Advanced.Core.Processor;
using AChain.Core.Context;
using AChain.Core.Result;
using AChain.Extensions;
using Contextillo;
using Microsoft.Extensions.Logging;

namespace AChain.Advanced.Core.Chain;

public abstract class ChainAdvancedBase<TContext> : IChainAdvanced<TContext> where TContext : class, IProcessContext
{
    private IProcessorAdvanced<TContext>? _firstProcessor;
    private readonly ILogger<ChainAdvancedBase<TContext>>? _logger;

    protected ChainAdvancedBase(ILogger<ChainAdvancedBase<TContext>>? logger = null)
    {
        _logger = logger;
    }

    public abstract IEnumerable<IProcessorAdvanced<TContext>> GetProcessors();

    public abstract IChainDispatcher<TContext> GetDispatcher();

    public void SetDispatcherEveryProcessor()
    {
        GetProcessors()
            .ToList()
            .ForEach(processor => processor.SetDispatcher(GetDispatcher()));
    }

    public virtual void Process(TContext context)
    {
        using (Context<List<string>>.Init(new List<string>()))
        {
            Context<List<string>>.Peek().Add(ToString() ?? "");

            try
            {
                GetFirstProcessor(context).Process(context);
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
                await GetFirstProcessor(context).ProcessAsync(context);
            }
            finally
            {
                _logger?.LogChain(Context<List<string>>.Peek());
            }
        }
    }

    private IProcessorAdvanced<TContext> GetFirstProcessor(TContext context)
    {
        if (_firstProcessor is null)
        {
            SetDispatcherEveryProcessor();
            _firstProcessor = GetDispatcher().GetFirstProcessor(context);
        }

        return _firstProcessor;
    }
}

public abstract class ChainAdvancedBase<TContext, TResult> : IChainAdvanced<TContext, TResult>
    where TContext : class, IProcessContext
    where TResult : class, IProcessResult
{
    private IProcessorAdvanced<TContext, TResult>? _firstProcessor;
    private readonly ILogger<ChainAdvancedBase<TContext, TResult>>? _logger;

    protected ChainAdvancedBase(ILogger<ChainAdvancedBase<TContext, TResult>>? logger = null)
    {
        _logger = logger;
    }

    public abstract IEnumerable<IProcessorAdvanced<TContext, TResult>> GetProcessors();

    public abstract IChainDispatcher<TContext, TResult> GetDispatcher();

    public void SetDispatcherEveryProcessor()
    {
        GetProcessors()
            .ToList()
            .ForEach(processor => processor.SetDispatcher(GetDispatcher()));
    }

    public virtual TResult Process(TContext context)
    {
        using (Context<List<string>>.Init(new List<string>()))
        {
            Context<List<string>>.Peek().Add(ToString() ?? "");

            try
            {
                return GetFirstProcessor(context).Process(context);
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
                return await GetFirstProcessor(context).ProcessAsync(context);
            }
            finally
            {
                _logger?.LogChain(Context<List<string>>.Peek());
            }
        }
    }

    private IProcessorAdvanced<TContext, TResult> GetFirstProcessor(TContext context)
    {
        if (_firstProcessor is null)
        {
            SetDispatcherEveryProcessor();
            _firstProcessor = GetDispatcher().GetFirstProcessor(context);
        }

        return _firstProcessor;
    }
}
