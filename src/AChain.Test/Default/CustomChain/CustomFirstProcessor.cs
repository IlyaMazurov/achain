using AChain.Core.Processor;

namespace AChain.Test.Default.CustomChain;

[ProcessorInChain(1)]
public class CustomFirstProcessor : ProcessorBase<CustomProcessContext>
{
    public override void Process(CustomProcessContext context)
    {
        Console.WriteLine("Первый кастомный обработчик");

        base.Process(context);
    }
}
