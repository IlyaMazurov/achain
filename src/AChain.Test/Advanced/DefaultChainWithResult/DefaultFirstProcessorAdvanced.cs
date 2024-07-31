using AChain.Advanced.Core.Processor;

namespace AChain.Test.Advanced.DefaultChainWithResult;

[ProcessorInChainAdvanced]
public class DefaultFirstProcessorAdvanced : ProcessorAdvancedBase<DefaultProcessContext, DefaultProcessResult>
{
    public override DefaultProcessResult Process(DefaultProcessContext context)
    {
        Console.WriteLine("Первый стандартный расширенный обработчик с ответом");

        context.CurrentNumber = 1;

        return base.Process(context);
    }
}
