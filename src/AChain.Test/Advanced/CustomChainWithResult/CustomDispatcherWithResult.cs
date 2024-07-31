using AChain.Advanced.Core.Dispatcher;
using AChain.Advanced.Core.Processor;
using AChain.Advanced.Extensions;

namespace AChain.Test.Advanced.CustomChainWithResult;

[ChainDispatcher]
public class CustomDispatcherWithResult : IChainDispatcher<CustomProcessContext, CustomProcessResult>
{
    private readonly IEnumerable<IProcessorAdvanced<CustomProcessContext, CustomProcessResult>> _processors;
    public CustomDispatcherWithResult(IEnumerable<IProcessorAdvanced<CustomProcessContext, CustomProcessResult>> processors)
    {
        _processors = processors;
    }

    public IProcessorAdvanced<CustomProcessContext, CustomProcessResult>? GetProcessor(CustomProcessContext context, IProcessorAdvanced<CustomProcessContext, CustomProcessResult> currentProcessor)
    {
        if (context.CurrentNumber == 0)
        {
            return _processors.GetProcessorByType(typeof(CustomFirstProcessorWithResult));
        }

        if (context.CurrentNumber == 1)
        {
            return _processors.GetProcessorByType(typeof(CustomSecondProcessorWithResult));
        }

        return default;
    }

    public IProcessorAdvanced<CustomProcessContext, CustomProcessResult> GetFirstProcessor(CustomProcessContext context)
    {
        return _processors.GetProcessorByType(typeof(CustomFirstProcessorWithResult));
    }
}
