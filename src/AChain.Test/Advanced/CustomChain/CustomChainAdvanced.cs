using AChain.Advanced.Core.Chain;
using AChain.Advanced.Core.Dispatcher;
using AChain.Advanced.Core.Processor;
using AChain.Advanced.Service;

namespace AChain.Test.Advanced.CustomChain;

[ChainAdvanced]
public class CustomChainAdvanced : ChainAdvanced<CustomProcessContext>
{
    public CustomChainAdvanced(
        IChainDispatcher<CustomProcessContext> dispatcher,
        IEnumerable<IProcessorAdvanced<CustomProcessContext>> processors) : base(dispatcher, processors) { }

    public override void Process(CustomProcessContext context)
    {
        Console.WriteLine("Кастомная цепочка без возвращения результата");

        base.Process(context);
    }
}
