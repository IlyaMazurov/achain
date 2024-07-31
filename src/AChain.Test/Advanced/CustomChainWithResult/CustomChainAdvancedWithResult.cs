using AChain.Advanced.Core.Chain;
using AChain.Advanced.Core.Dispatcher;
using AChain.Advanced.Core.Processor;
using AChain.Advanced.Service;

namespace AChain.Test.Advanced.CustomChainWithResult;

[ChainAdvanced]
public class CustomChainAdvancedWithResult : ChainAdvanced<CustomProcessContext, CustomProcessResult>
{
    public CustomChainAdvancedWithResult(
        IChainDispatcher<CustomProcessContext, CustomProcessResult> dispatcher,
        IEnumerable<IProcessorAdvanced<CustomProcessContext, CustomProcessResult>> processors) : base(dispatcher, processors) { }

    public override CustomProcessResult Process(CustomProcessContext context)
    {
        Console.WriteLine("Кастомная цепочка с возвращением результата");

        return base.Process(context);
    }
}
