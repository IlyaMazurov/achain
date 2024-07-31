using AChain.Core.Result;

namespace AChain.Test.Advanced.DefaultChainWithResult;

public class DefaultProcessResult : IProcessResult
{
    public string? Result { get; set; }

    public DefaultProcessResult(string? result)
    {
        Result = result;
    }
}
