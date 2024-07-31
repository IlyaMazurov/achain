using AChain.Core.Context;

namespace AChain.Test.Advanced.DefaultChainWithResult;

public class DefaultProcessContext : IProcessContext
{
    public ushort CurrentNumber { get; set; }
}
