using AChain.Advanced.Core.Processor;

namespace AChain.Test.Advanced.CustomChain;

[ProcessorInChainAdvanced]
public class CustomFirstProcessorAdvanced : ProcessorAdvancedBase<CustomProcessContext>
{
    public override void Process(CustomProcessContext context)
    {
        Console.WriteLine("Первый кастомный обработчик без возвращения результата");

        context.CurrentNumber = 1;

        base.Process(context);
    }
}
