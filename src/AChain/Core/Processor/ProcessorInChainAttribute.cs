namespace AChain.Core.Processor;

[AttributeUsage(AttributeTargets.Class)]
public sealed class ProcessorInChainAttribute : Attribute
{
    public ushort Number { get; }

    public ProcessorInChainAttribute(ushort number)
    {
        Number = number;
    }
}
