using AChain.Core.Processor;

namespace AChain.Test.Default.CustomChainWithResult;

[ProcessorInChain(2)]
public class CustomSecondProcessor : ProcessorBase<CustomProcessContext, CustomProcessResult>
{
    public override CustomProcessResult Process(CustomProcessContext context)
    {
        Console.WriteLine("Второй кастомный обработчик с результатом");

        Result = new CustomProcessResult("result");

        return base.Process(context);
    }
}
