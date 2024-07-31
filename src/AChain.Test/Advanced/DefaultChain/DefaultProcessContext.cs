using AChain.Core.Context;

namespace AChain.Test.Advanced.DefaultChain;

public class DefaultProcessContext : IProcessContext
{
    public ushort CurrentNumber { get; set; }
}
