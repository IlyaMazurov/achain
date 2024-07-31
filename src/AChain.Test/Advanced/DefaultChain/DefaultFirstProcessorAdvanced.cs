using AChain.Advanced.Core.Processor;

namespace AChain.Test.Advanced.DefaultChain;

[ProcessorInChainAdvanced]
public class DefaultFirstProcessorAdvanced : ProcessorAdvancedBase<DefaultProcessContext>
{
    public override void Process(DefaultProcessContext context)
    {
        Console.WriteLine("Первый стандартный расширенный обработчик без ответа");

        context.CurrentNumber = 1;

        base.Process(context);
    }
}
