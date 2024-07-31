﻿using AChain.Advanced.Core.Chain;
using AChain.Advanced.Core.Dispatcher;
using AChain.Advanced.Core.Processor;
using AChain.Test.Advanced.Configuration;
using AChain.Test.Advanced.DefaultChainWithResult;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace AChain.Test.Advanced;

[TestFixture]
public class DefaultChainAdvancedWithResultTest
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
        var processors = _serviceProvider.GetRequiredService<IEnumerable<IProcessorAdvanced<DefaultProcessContext, DefaultProcessResult>>>().ToList();

        var firstProcessor = processors.FirstOrDefault(p => p.GetType() == typeof(DefaultFirstProcessorAdvanced));
        var secondProcessor = processors.FirstOrDefault(p => p.GetType() == typeof(DefaultSecondProcessorAdvanced));

        Assert.Multiple(() =>
        {
            Assert.That(firstProcessor, Is.Not.Null);
            Assert.That(secondProcessor, Is.Not.Null);
        });
    }

    [Test]
    public void RegisterDispatcherTest()
    {
        var dispatcher = _serviceProvider.GetRequiredService<IChainDispatcher<DefaultProcessContext, DefaultProcessResult>>();

        Assert.That(dispatcher, Is.Not.Null);
    }

    [Test]
    public void RegisterChainTest()
    {
        var chain = _serviceProvider.GetRequiredService<IChainAdvanced<DefaultProcessContext, DefaultProcessResult>>();
        Assert.That(chain, Is.Not.Null);
    }

    [Test]
    public void ProcessChainTest()
    {
        var chain = _serviceProvider.GetRequiredService<IChainAdvanced<DefaultProcessContext, DefaultProcessResult>>();
        var context = new DefaultProcessContext();

        var result = chain.Process(context);

        Assert.That(result.Result, Is.EqualTo("result"));
    }
}