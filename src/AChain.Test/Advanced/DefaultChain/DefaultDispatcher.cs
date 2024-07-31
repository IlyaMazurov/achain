using AChain.Advanced.Core.Dispatcher;
using AChain.Advanced.Core.Processor;
using AChain.Advanced.Extensions;

namespace AChain.Test.Advanced.DefaultChain;

[ChainDispatcher]
public class DefaultDispatcher : IChainDispatcher<DefaultProcessContext>
{
    private readonly IEnumerable<IProcessorAdvanced<DefaultProcessContext>> _processors;
    public DefaultDispatcher(IEnumerable<IProcessorAdvanced<DefaultProcessContext>> processors)
    {
        _processors = processors;
    }

    public IProcessorAdvanced<DefaultProcessContext>? GetProcessor(DefaultProcessContext context, IProcessorAdvanced<DefaultProcessContext> currentProcessor)
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

    public IProcessorAdvanced<DefaultProcessContext> GetFirstProcessor(DefaultProcessContext context)
    {
        return _processors.GetProcessorByType(typeof(DefaultFirstProcessorAdvanced));
    }
}
