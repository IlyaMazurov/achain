using AChain.Core.Processor;

#pragma warning disable CA1716

namespace AChain.Test.Default.DefaultChainWithResult;

[ProcessorInChain(1)]
public class DefaultFirstProcessor : ProcessorBase<DefaultProcessContext, DefaultProcessResult>
{
    public override DefaultProcessResult Process(DefaultProcessContext context)
    {
        Console.WriteLine("Первый обработчик с результатом");

        return base.Process(context);
    }
}
