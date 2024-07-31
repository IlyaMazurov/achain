using AChain.Core.Processor;

namespace AChain.Test.Default.CustomChain;

[ProcessorInChain(2)]
public class CustomSecondProcessor : ProcessorBase<CustomProcessContext>
{
    public override void Process(CustomProcessContext context)
    {
        Console.WriteLine("Второй кастомный обработчик");

        base.Process(context);
    }
}
