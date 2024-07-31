using AChain.Core.Processor;

#pragma warning disable CA1716

namespace AChain.Test.Default.DefaultChain;

[ProcessorInChain(1)]
public class DefaultFirstProcessor : ProcessorBase<DefaultProcessContext>
{
    public override void Process(DefaultProcessContext context)
    {
        Console.WriteLine("Первый обработчик");

        base.Process(context);
    }
}
