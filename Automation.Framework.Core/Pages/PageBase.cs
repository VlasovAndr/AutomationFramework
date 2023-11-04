using Automation.Framework.Common.Abstractions;
using Automation.Framework.Core.Configuration;

namespace Automation.Framework.Core.Pages;

public class PageBase
{
    protected readonly IWebDriverWrapper browser;
    protected readonly ILogging log;
    private readonly TestRunConfiguration config;
    protected string BaseUrl => config.TargetEnvironment.Url;

    public PageBase(IWebDriverWrapper browser, ILogging log, TestRunConfiguration config)
    {
        this.browser = browser;
        this.log = log;
        this.config = config;
    }

    public string GetTitle()
    {
        var title = browser.WebDriver.Title;
        log.Information($"Execute GetTitle method. Current title - {title}");
        return title;
    }

    public void NavigateToUrl(string url)
    {
        browser.NavigateToUrl(url);
        log.Information($"Execute NavigateToUrl BrowserAction method");
    }

    public void GoToTab(int tabIndex)
    {
        browser.GoToTab(tabIndex);
        log.Information($"Execute GoToTab BrowserAction method");
    }

    public void CloseCurrentTab()
    {
        browser.CloseCurrentTab();
        log.Information($"Execute CloseCurrentTab BrowserAction method");
    }

    public void Close()
    {
        browser.CloseDriver();
        log.Information($"Execute Close BrowserAction method");
    }
}
