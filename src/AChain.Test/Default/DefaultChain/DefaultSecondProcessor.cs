using AChain.Core.Processor;

namespace AChain.Test.Default.DefaultChain;

[ProcessorInChain(2)]
public class DefaultSecondProcessor : ProcessorBase<DefaultProcessContext>
{
    public override void Process(DefaultProcessContext context)
    {
        Console.WriteLine("Второй обработчик");

        base.Process(context);
    }
}
