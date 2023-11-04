using Automation.Framework.Common.Abstractions;
using Automation.Framework.Core.Configuration;
using Automation.Framework.Core.Dependencies;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace AutomationTestsPOM;


public class TestBase
{
    public IServiceProvider container;
    public ILogging log;

    public TestBase()
    {
        container = DIContainer.ConfigureServices();
        log = container.GetRequiredService<ILogging>();
        var config = container.GetRequiredService<TestRunConfiguration>();
        log.SetLogLevel(config.Framework.LogLevel);
    }

    [TearDown]
    public void AfterEach()
    {
        log.Debug("Starting AfterEach teardown");

        var webDriver = container.GetRequiredService<IWebDriverWrapper>();
        var outcome = TestContext.CurrentContext.Result.Outcome.Status;

        if (outcome == TestStatus.Passed)
        {
            log.Information("Outcome: Passed");
        }
        else if (outcome == TestStatus.Failed)
        {
            //webDriver.TakeScreenshot($"test_failed + {TestContext.CurrentContext.Test.FullName}");
            log.Information($"Test failed for reason: {TestContext.CurrentContext.Result.Message}");
        }
        else
        {
            log.Warning("Outcome: " + outcome);
        }

        try
        {
            if (webDriver.IsWebDriverCreated)
                webDriver.CloseDriver();
        }
        catch (Exception ex)
        {
            log.Error($"Failed on closing web driver on after test run event. {ex.Message}");
        }
    }
}
