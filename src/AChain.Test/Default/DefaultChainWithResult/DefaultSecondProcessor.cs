using AChain.Core.Processor;

namespace AChain.Test.Default.DefaultChainWithResult;

[ProcessorInChain(2)]
public class DefaultSecondProcessor : ProcessorBase<DefaultProcessContext, DefaultProcessResult>
{
    public override DefaultProcessResult Process(DefaultProcessContext context)
    {
        Console.WriteLine("Второй обработчик с результатом");

        return new DefaultProcessResult("result");
    }
}
