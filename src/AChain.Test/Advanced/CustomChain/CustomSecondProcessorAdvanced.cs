using AChain.Advanced.Core.Processor;

namespace AChain.Test.Advanced.CustomChain;

[ProcessorInChainAdvanced]
public class CustomSecondProcessorAdvanced : ProcessorAdvancedBase<CustomProcessContext>
{
    public override void Process(CustomProcessContext context)
    {
        Console.WriteLine("Второй кастомный обработчик без возвращения результата");

        context.CurrentNumber = 2;

        base.Process(context);
    }
}
