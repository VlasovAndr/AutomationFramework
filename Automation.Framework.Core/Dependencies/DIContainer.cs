using Automation.Framework.Common.Abstractions;
using Automation.Framework.Common.Params;
using Automation.Framework.Common.Reports;
using Automation.Framework.Core.Configuration;
using Automation.Framework.Core.Pages;
using Automation.Framework.Core.Pages.Components;
using Automation.Framework.Core.Pages.Locators;
using Automation.Framework.Core.WebUI.Selenium;
using Automation.Framework.Core.WebUI.Selenium.WebDriverFactory;
using Microsoft.Extensions.DependencyInjection;

namespace Automation.Framework.Core.Dependencies;

public class DIContainer
{
    public static IServiceCollection serviceCollection;

    public static IServiceProvider ConfigureServices()
    {
        if (serviceCollection == null)
        {
            serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IDefaultVariables, DefaultVariables>();
            serviceCollection.AddSingleton<ILogging, ConsoleLogger>(); // [ConsoleLogger/FileLogger/SpecflowLogger/]
            serviceCollection.AddSingleton<TestRunConfiguration>();
            serviceCollection.AddSingleton<INamedBrowserFactory, ChromeDriverFactory>();
            serviceCollection.AddSingleton<INamedBrowserFactory, FirefoxDriverFactory>();
            serviceCollection.AddSingleton<IWebDriverWrapper, WebDriverWrapper>();

            serviceCollection.AddSingleton<PageBase>();
            serviceCollection.AddSingleton<HomePage>();
            serviceCollection.AddSingleton<HomePageLocators>();
            serviceCollection.AddSingleton<ElementsPage>();
            serviceCollection.AddSingleton<ElementsLocators>();
            serviceCollection.AddSingleton<LeftNavPanel>();
            serviceCollection.AddSingleton<LeftNavPanelLocators>();
        }

        return serviceCollection.BuildServiceProvider();
    }
}
