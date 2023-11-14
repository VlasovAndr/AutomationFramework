using Automation.Framework.Common.Abstractions;
using Automation.Framework.Core.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Firefox;

namespace Automation.Framework.Core.WebUI.Selenium.WebDriverFactory;

public class RemoteFirefoxDriverFactory : INamedBrowserFactory
{
    public BrowserName Name => BrowserName.Chrome;
    public BrowserType Type => BrowserType.Remote;
    private TestRunConfiguration testRunConfiguration;
    private ILogging log;

    public RemoteFirefoxDriverFactory(TestRunConfiguration testRunConfiguration, ILogging log)
    {
        this.testRunConfiguration = testRunConfiguration;
        this.log = log;
    }

    public IWebDriver Create()
    {
        log.Debug("Creating Remote Firefox Driver");

        var options = new FirefoxOptions();
        options.SetPreference("browser.download.prompt_for_download", false);
        options.SetPreference("pdfjs.disabled", true);  // to always open PDF externally
        options.SetPreference("browser.download.manager.showWhenStarting", false);
        options.SetPreference("browser.safebrowsing.enabled", true);
        options.AddArgument("no-sandbox");
        options.AddArgument("--disable-gpu");
        options.AddArgument("--disable-popup-blocking");
        options.SetPreference("browser.download.dir", testRunConfiguration.Framework.DownloadedLocation);
        options.SetPreference("network.cookie.cookieBehavior", 0);
        if (testRunConfiguration.Driver.Headless) options.AddArgument("--headless=new");

        options.AddAdditionalOption("selenoid:options", new Dictionary<string, object>
        {
            ["enableLog"] = true,
            ["enableVnc"] = true,
            ["enableVideo"] = false
        });

        var webDriver = new RemoteWebDriver(new Uri($"{testRunConfiguration.Driver.GridHubUrl}"), options.ToCapabilities());
        webDriver.Manage().Window.Maximize();

        return webDriver;
    }
}
