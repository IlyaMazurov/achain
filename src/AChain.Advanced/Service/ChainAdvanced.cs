using AChain.Advanced.Core.Chain;
using AChain.Advanced.Core.Dispatcher;
using AChain.Advanced.Core.Processor;
using AChain.Core.Context;
using AChain.Core.Result;
using Microsoft.Extensions.Logging;

namespace AChain.Advanced.Service;

[ChainAdvanced]
public class ChainAdvanced<TContext> : ChainAdvancedBase<TContext>
    where TContext : class, IProcessContext
{
    private readonly IChainDispatcher<TContext> _dispatcher;
    private readonly IEnumerable<IProcessorAdvanced<TContext>> _processors;

    public ChainAdvanced(IChainDispatcher<TContext> dispatcher,
        IEnumerable<IProcessorAdvanced<TContext>> processors,
        ILogger<ChainAdvanced<TContext>>? logger = null) : base(logger)
    {
        _dispatcher = dispatcher;
        _processors = processors;
    }

    public override IEnumerable<IProcessorAdvanced<TContext>> GetProcessors() => _processors;

    public override IChainDispatcher<TContext> GetDispatcher() => _dispatcher;
}

[ChainAdvanced]
public class ChainAdvanced<TContext, TResult> : ChainAdvancedBase<TContext, TResult>
    where TContext : class, IProcessContext
    where TResult : class, IProcessResult
{
    private readonly IChainDispatcher<TContext, TResult> _dispatcher;
    private readonly IEnumerable<IProcessorAdvanced<TContext, TResult>> _processors;

    public ChainAdvanced(IChainDispatcher<TContext, TResult> dispatcher,
        IEnumerable<IProcessorAdvanced<TContext, TResult>> processors,
        ILogger<ChainAdvanced<TContext, TResult>>? logger = null) : base(logger)
    {
        _dispatcher = dispatcher;
        _processors = processors;
    }

    public override IEnumerable<IProcessorAdvanced<TContext, TResult>> GetProcessors() => _processors;

    public override IChainDispatcher<TContext, TResult> GetDispatcher() => _dispatcher;
}
