using AChain.Core.Chain;
using AChain.Core.Context;
using AChain.Core.Processor;
using AChain.Core.Result;
using Microsoft.Extensions.Logging;

#pragma warning disable CA1724

namespace AChain.Service;

[Chain]
public class Chain<TContext> : ChainBase<TContext>
    where TContext : class, IProcessContext
{
    private readonly IEnumerable<IProcessor<TContext>> _processors;

    public Chain(IEnumerable<IProcessor<TContext>> processors, ILogger<Chain<TContext>>? logger = null) : base(logger)
    {
        _processors = processors;
    }

    protected override IEnumerable<IProcessor<TContext>> GetProcessors()
    {
        return _processors;
    }
}

[Chain]
public class Chain<TContext, TResult> : ChainBase<TContext, TResult>
    where TContext : class, IProcessContext
    where TResult : class, IProcessResult
{
    private readonly IEnumerable<IProcessor<TContext, TResult>> _processors;

    public Chain(IEnumerable<IProcessor<TContext, TResult>> processors, ILogger<Chain<TContext, TResult>>? logger = null) : base(logger)
    {
        _processors = processors;
    }

    protected override IEnumerable<IProcessor<TContext, TResult>> GetProcessors()
    {
        return _processors;
    }
}
