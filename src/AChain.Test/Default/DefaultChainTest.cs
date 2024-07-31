using AChain.Core.Chain;
using AChain.Core.Processor;
using AChain.Test.Default.DefaultChain;
using AChain.Test.Default.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace AChain.Test.Default;

[TestFixture]
public class DefaultChainTest
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
        var processors = _serviceProvider.GetRequiredService<IEnumerable<IProcessor<DefaultProcessContext>>>().ToList();

        var firstProcessor = processors.FirstOrDefault(p => p.GetType() == typeof(DefaultFirstProcessor));
        var secondProcessor = processors.FirstOrDefault(p => p.GetType() == typeof(DefaultSecondProcessor));

        Assert.Multiple(() =>
        {
            Assert.That(firstProcessor, Is.Not.Null);
            Assert.That(secondProcessor, Is.Not.Null);
        });
    }

    [Test]
    public void RegisterChainTest()
    {
        var chain = _serviceProvider.GetRequiredService<IChain<DefaultProcessContext>>();
        Assert.That(chain, Is.Not.Null);
    }

    [Test]
    public void ProcessChainTest()
    {
        var chain = _serviceProvider.GetRequiredService<IChain<DefaultProcessContext>>();
        var context = new DefaultProcessContext();

        Assert.DoesNotThrow(() => chain.Process(context));
    }
}
