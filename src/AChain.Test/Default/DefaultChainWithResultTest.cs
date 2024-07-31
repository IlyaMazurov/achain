using AChain.Core.Chain;
using AChain.Core.Processor;
using AChain.Test.Default.Configuration;
using AChain.Test.Default.DefaultChainWithResult;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace AChain.Test.Default;

[TestFixture]
public class DefaultChainWithResultTest
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
        var processors = _serviceProvider.GetRequiredService<IEnumerable<IProcessor<DefaultProcessContext, DefaultProcessResult>>>().ToList();

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
        var chain = _serviceProvider.GetRequiredService<IChain<DefaultProcessContext, DefaultProcessResult>>();
        Assert.That(chain, Is.Not.Null);
    }

    [Test]
    public void ProcessChainTest()
    {
        var chain = _serviceProvider.GetRequiredService<IChain<DefaultProcessContext, DefaultProcessResult>>();
        var context = new DefaultProcessContext();

        var result = chain.Process(context);

        Assert.That(result.Result, Is.EqualTo("result"));
    }
}
