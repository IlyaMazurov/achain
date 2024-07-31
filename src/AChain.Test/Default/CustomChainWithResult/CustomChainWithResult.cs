using AChain.Core.Chain;
using AChain.Core.Processor;
using AChain.Service;

#pragma warning disable CA1716
#pragma warning disable CA1724

namespace AChain.Test.Default.CustomChainWithResult;

[Chain]
public class CustomChainWithResult : Chain<CustomProcessContext, CustomProcessResult>
{
    public CustomChainWithResult(IEnumerable<IProcessor<CustomProcessContext, CustomProcessResult>> processors) : base(processors) { }

    public override CustomProcessResult Process(CustomProcessContext context)
    {
        Console.WriteLine("Кастомная цепочка с результатом");

        return base.Process(context);
    }
}
