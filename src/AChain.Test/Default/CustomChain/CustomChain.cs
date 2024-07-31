using AChain.Core.Chain;
using AChain.Core.Processor;
using AChain.Service;
using Microsoft.Extensions.Logging;

#pragma warning disable CA1716
#pragma warning disable CA1724

namespace AChain.Test.Default.CustomChain;

[Chain]
public class CustomChain(IEnumerable<IProcessor<CustomProcessContext>> processors, ILogger<CustomChain> logger)
    : Chain<CustomProcessContext>(processors, logger)
{
    public override void Process(CustomProcessContext context)
    {
        Console.WriteLine("Кастомная цепочка");

        base.Process(context);
    }
}
