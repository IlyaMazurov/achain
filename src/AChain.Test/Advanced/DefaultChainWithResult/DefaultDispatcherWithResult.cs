using AChain.Advanced.Core.Dispatcher;
using AChain.Advanced.Core.Processor;
using AChain.Advanced.Extensions;

namespace AChain.Test.Advanced.DefaultChainWithResult;

[ChainDispatcher]
public class DefaultDispatcherWithResult : IChainDispatcher<DefaultProcessContext, DefaultProcessResult>
{
    private readonly IEnumerable<IProcessorAdvanced<DefaultProcessContext, DefaultProcessResult>> _processors;
    public DefaultDispatcherWithResult(IEnumerable<IProcessorAdvanced<DefaultProcessContext, DefaultProcessResult>> processors)
    {
        _processors = processors;
    }

    public IProcessorAdvanced<DefaultProcessContext, DefaultProcessResult>? GetProcessor(DefaultProcessContext context, IProcessorAdvanced<DefaultProcessContext, DefaultProcessResult> currentProcessor)
    {
        if (context.CurrentNumber == 0)
        {
            return _processors.GetProcessorByType(typeof(DefaultFirstProcessorAdvanced));
        }

        if (context.CurrentNumber == 1)
        {
            return _processors.GetProcessorByType(typeof(DefaultSecondProcessorAdvanced));
        }

        return default;
    }

    public IProcessorAdvanced<DefaultProcessContext, DefaultProcessResult> GetFirstProcessor(DefaultProcessContext context)
    {
        return _processors.GetProcessorByType(typeof(DefaultFirstProcessorAdvanced));
    }
}
