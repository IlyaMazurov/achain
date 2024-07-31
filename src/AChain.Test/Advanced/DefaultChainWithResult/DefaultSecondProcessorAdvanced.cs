using AChain.Advanced.Core.Processor;

namespace AChain.Test.Advanced.DefaultChainWithResult;

[ProcessorInChainAdvanced]
public class DefaultSecondProcessorAdvanced : ProcessorAdvancedBase<DefaultProcessContext, DefaultProcessResult>
{
    public override DefaultProcessResult Process(DefaultProcessContext context)
    {
        Console.WriteLine("Второй стандартный расширенный обработчик с ответом");

        context.CurrentNumber = 2;

        Result = new DefaultProcessResult("result");

        return base.Process(context);
    }
}
