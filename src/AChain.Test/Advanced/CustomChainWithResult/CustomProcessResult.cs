using AChain.Core.Result;

namespace AChain.Test.Advanced.CustomChainWithResult;

public class CustomProcessResult : IProcessResult
{
    public string? Result { get; set; }

    public CustomProcessResult(string? result)
    {
        Result = result;
    }
}
