using Automation.Framework.Common.Abstractions;
using Automation.Framework.Core.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Automation.Framework.Core.WebUI.Selenium.WebDriverFactory;

public class ChromeDriverFactory : INamedBrowserFactory
{
    public Browsers Name => Browsers.Chrome;
    private TestRunConfiguration testRunConfiguration;
    private ILogging log;

    public ChromeDriverFactory(TestRunConfiguration testRunConfiguration, ILogging log)
    {
        this.testRunConfiguration = testRunConfiguration;
        this.log = log;
    }

    public IWebDriver Create()
    {
        log.Debug("Creating ChromeDriver");

        var options = new ChromeOptions();
        options.AddUserProfilePreference("download.prompt_for_download", false);
        options.AddUserProfilePreference("plugins.always_open_pdf_externally", true);
        options.AddUserProfilePreference("browser.download.manager.showWhenStarting", false);
        options.AddUserProfilePreference("safebrowsing.enabled", "true");
        //"no-sandbox" and "--disable-gpu" must work together. Or it will need to delete.
        //"no-sandbox" parameter is usefull for running in container 
        options.AddArgument("no-sandbox");
        options.AddArgument("--disable-gpu");
        options.AddArgument("disable-popup-blocking");
        options.AddUserProfilePreference("download.default_directory", testRunConfiguration.Framework.DownloadedLocation);
        options.AddUserProfilePreference("profile.cookie_controls_mode", 0);
        if (testRunConfiguration.Driver.Headless) options.AddArgument("--headless=new");

        var specificDriver = new ChromeDriver(options);

        //specificDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        //specificDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
        //specificDriver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(10);
        specificDriver.Manage().Window.Maximize();

        return specificDriver;
    }
}
