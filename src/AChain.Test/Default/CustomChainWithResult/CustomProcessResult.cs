using AChain.Core.Result;

namespace AChain.Test.Default.CustomChainWithResult;

public class CustomProcessResult : IProcessResult
{
    public string? Name { get; set; }

    public CustomProcessResult(string? name)
    {
        Name = name;
    }
}
