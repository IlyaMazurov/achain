using AChain.Core.Context;

namespace AChain.Test.Advanced.CustomChain;

public class CustomProcessContext : IProcessContext
{
    public ushort CurrentNumber { get; set; }
}
