using AChain.Core.Processor;

namespace AChain.Test.Default.CustomChainWithResult;

[ProcessorInChain(1)]
public class CustomFirstProcessor : ProcessorBase<CustomProcessContext, CustomProcessResult>
{
    public override CustomProcessResult Process(CustomProcessContext context)
    {
        Console.WriteLine("Первый кастомный обработчик с результатом");

        return base.Process(context);
    }
}
