using Microsoft.Extensions.Logging;

namespace AChain.Extensions;

public static partial class LoggerExtensions
{
    [LoggerMessage(0, LogLevel.Debug, "Chain={chain}")]
    public static partial void LogChain(this ILogger logger, string chain);

    public static void LogChain(this ILogger logger, IEnumerable<string> values)
    {
        logger.LogChain(string.Join("\n-->", values));
    }
}
