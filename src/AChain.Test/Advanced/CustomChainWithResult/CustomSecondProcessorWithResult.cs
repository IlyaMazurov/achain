using AChain.Advanced.Core.Processor;

namespace AChain.Test.Advanced.CustomChainWithResult;

[ProcessorInChainAdvanced]
public class CustomSecondProcessorWithResult : ProcessorAdvancedBase<CustomProcessContext, CustomProcessResult>
{
    public override CustomProcessResult Process(CustomProcessContext context)
    {
        Console.WriteLine("Второй кастомный расширенный обработчик с возвращением результата");

        context.CurrentNumber = 1;

        return new CustomProcessResult("result");
    }
}
