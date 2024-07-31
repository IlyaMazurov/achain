using AChain.Advanced.Core.Chain;
using AChain.Advanced.Core.Dispatcher;
using AChain.Advanced.Core.Processor;
using AChain.Test.Advanced.Configuration;
using AChain.Test.Advanced.CustomChainWithResult;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace AChain.Test.Advanced;

[TestFixture]
public class CustomChainAdvancedWithResultTest
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
        var processors = _serviceProvider.GetRequiredService<IEnumerable<IProcessorAdvanced<CustomProcessContext, CustomProcessResult>>>().ToList();

        var firstProcessor = processors.FirstOrDefault(p => p.GetType() == typeof(CustomFirstProcessorWithResult));
        var secondProcessor = processors.FirstOrDefault(p => p.GetType() == typeof(CustomSecondProcessorWithResult));

        Assert.Multiple(() =>
        {
            Assert.That(firstProcessor, Is.Not.Null);
            Assert.That(secondProcessor, Is.Not.Null);
        });
    }

    [Test]
    public void RegisterDispatcherTest()
    {
        var dispatcher = _serviceProvider.GetRequiredService<IChainDispatcher<CustomProcessContext, CustomProcessResult>>();

        Assert.That(dispatcher, Is.Not.Null);
    }

    [Test]
    public void RegisterChainTest()
    {
        var chain = _serviceProvider.GetRequiredService<IChainAdvanced<CustomProcessContext, CustomProcessResult>>();
        Assert.That(chain, Is.Not.Null);
    }

    [Test]
    public void ProcessChainTest()
    {
        var chain = _serviceProvider.GetRequiredService<IChainAdvanced<CustomProcessContext, CustomProcessResult>>();
        var context = new CustomProcessContext();

        var result = chain.Process(context);

        Assert.That(result.Result, Is.EqualTo("result"));
    }
}
