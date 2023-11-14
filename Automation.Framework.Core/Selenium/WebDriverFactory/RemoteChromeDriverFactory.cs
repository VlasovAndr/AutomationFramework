﻿using Automation.Framework.Common.Abstractions;
using Automation.Framework.Core.Configuration;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Automation.Framework.Core.WebUI.Selenium.WebDriverFactory;

public class RemoteChromeDriverFactory : INamedBrowserFactory
{
    public BrowserName Name => BrowserName.Chrome;
    public BrowserType Type => BrowserType.Remote;
    private TestRunConfiguration testRunConfiguration;
    private ILogging log;

    public RemoteChromeDriverFactory(TestRunConfiguration testRunConfiguration, ILogging log)
    {
        this.testRunConfiguration = testRunConfiguration;
        this.log = log;
    }

    public IWebDriver Create()
    {
        log.Debug("Creating Remote Chrome Driver");

        var options = new ChromeOptions();
        options.AddUserProfilePreference("download.prompt_for_download", false);
        options.AddUserProfilePreference("plugins.always_open_pdf_externally", true);
        options.AddUserProfilePreference("browser.download.manager.showWhenStarting", false);
        options.AddUserProfilePreference("safebrowsing.enabled", "true");
        options.AddArgument("no-sandbox");
        options.AddArgument("--disable-gpu");
        options.AddArgument("disable-popup-blocking");
        options.AddUserProfilePreference("download.default_directory", testRunConfiguration.Framework.DownloadedLocation);
        options.AddUserProfilePreference("profile.cookie_controls_mode", 0);
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
