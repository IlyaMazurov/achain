using AChain.Core.Chain;
using AChain.Core.Processor;
using AChain.Test.Default.Configuration;
using AChain.Test.Default.CustomChain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace AChain.Test.Default;

[TestFixture]
public class CustomChainTest
{
    private IServiceProvider _serviceProvider = null!;

    [SetUp]
    public void SetUp()
    {
        _serviceProvider = ServiceProviderConfigurator.Configure();
    }

    [Test]
    public void RegisterProcessorsTest()
    {
        var processors = _serviceProvider.GetRequiredService<IEnumerable<IProcessor<CustomProcessContext>>>().ToList();

        var firstProcessor = processors.FirstOrDefault(p => p.GetType() == typeof(CustomFirstProcessor));
        var secondProcessor = processors.FirstOrDefault(p => p.GetType() == typeof(CustomSecondProcessor));

        Assert.Multiple(() =>
        {
            Assert.That(firstProcessor, Is.Not.Null);
            Assert.That(secondProcessor, Is.Not.Null);
        });
    }

    [Test]
    public void RegisterChainTest()
    {
        var chain = _serviceProvider.GetRequiredService<IChain<CustomProcessContext>>();
        Assert.That(chain, Is.Not.Null);
    }

    [Test]
    public void ProcessChainTest()
    {
        var chain = _serviceProvider.GetRequiredService<IChain<CustomProcessContext>>();
        _ = _serviceProvider.GetRequiredService<ILogger<CustomChainTest>>();

        var context = new CustomProcessContext();

        Assert.DoesNotThrow(() => chain.Process(context));
    }
}
