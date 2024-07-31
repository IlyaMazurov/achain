using AChain.Core.Context;

namespace AChain.Test.Advanced.CustomChainWithResult;

public class CustomProcessContext : IProcessContext
{
    public ushort CurrentNumber { get; set; }
}
