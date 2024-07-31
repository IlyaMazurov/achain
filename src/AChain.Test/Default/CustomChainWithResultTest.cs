using AChain.Core.Chain;
using AChain.Core.Processor;
using AChain.Test.Default.Configuration;
using AChain.Test.Default.CustomChainWithResult;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace AChain.Test.Default;

[TestFixture]
public class CustomChainWithResultTest
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
        var processors = _serviceProvider.GetRequiredService<IEnumerable<IProcessor<CustomProcessContext, CustomProcessResult>>>().ToList();

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
        var chain = _serviceProvider.GetRequiredService<IChain<CustomProcessContext, CustomProcessResult>>();
        Assert.That(chain, Is.Not.Null);
    }

    [Test]
    public void ProcessChainTest()
    {
        var chain = _serviceProvider.GetRequiredService<IChain<CustomProcessContext, CustomProcessResult>>();
        var context = new CustomProcessContext();

        var result = chain.Process(context);

        Assert.That(result.Name, Is.EqualTo("result"));
    }
}
