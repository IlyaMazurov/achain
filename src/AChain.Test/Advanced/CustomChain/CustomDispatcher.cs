using AChain.Advanced.Core.Dispatcher;
using AChain.Advanced.Core.Processor;
using AChain.Advanced.Extensions;

namespace AChain.Test.Advanced.CustomChain;

[ChainDispatcher]
public class CustomDispatcher : IChainDispatcher<CustomProcessContext>
{
    private readonly IEnumerable<IProcessorAdvanced<CustomProcessContext>> _processors;

    public CustomDispatcher(IEnumerable<IProcessorAdvanced<CustomProcessContext>> processors)
    {
        _processors = processors;
    }

    public IProcessorAdvanced<CustomProcessContext>? GetProcessor(CustomProcessContext context, IProcessorAdvanced<CustomProcessContext> currentProcessor)
    {
        if (context.CurrentNumber == 0)
        {
            return _processors.GetProcessorByType(typeof(CustomFirstProcessorAdvanced));
        }

        if (context.CurrentNumber == 1)
        {
            return _processors.GetProcessorByType(typeof(CustomSecondProcessorAdvanced));
        }

        return default;
    }

    public IProcessorAdvanced<CustomProcessContext> GetFirstProcessor(CustomProcessContext context)
    {
        return _processors.GetProcessorByType(typeof(CustomFirstProcessorAdvanced));
    }
}
