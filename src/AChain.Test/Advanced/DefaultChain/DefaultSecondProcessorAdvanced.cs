using AChain.Advanced.Core.Processor;

namespace AChain.Test.Advanced.DefaultChain;

[ProcessorInChainAdvanced]
public class DefaultSecondProcessorAdvanced : ProcessorAdvancedBase<DefaultProcessContext>
{
    public override void Process(DefaultProcessContext context)
    {
        Console.WriteLine("Второй стандартный расширенный обработчик без ответа");

        context.CurrentNumber = 2;

        base.Process(context);
    }
}
