using AChain.Advanced.Core.Processor;

namespace AChain.Test.Advanced.CustomChainWithResult;

[ProcessorInChainAdvanced]
public class CustomFirstProcessorWithResult : ProcessorAdvancedBase<CustomProcessContext, CustomProcessResult>
{
    public override CustomProcessResult Process(CustomProcessContext context)
    {
        Console.WriteLine("Первый кастомный расширенный обработчик с возвращением результата");

        context.CurrentNumber = 1;

        return base.Process(context);
    }
}
