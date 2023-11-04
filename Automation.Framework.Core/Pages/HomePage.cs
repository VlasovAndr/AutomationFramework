using Automation.Framework.Common.Abstractions;
using Automation.Framework.Core.Configuration;
using Automation.Framework.Core.Pages.Locators;

namespace Automation.Framework.Core.Pages;

public class HomePage : PageBase
{
    private readonly HomePageLocators repo;

    public HomePage(HomePageLocators repo, IWebDriverWrapper browser, ILogging log, TestRunConfiguration config)
        : base(browser, log, config)
    {
        this.repo = repo;
    }

    public void Open()
    {
        browser.NavigateToUrl(BaseUrl);
        log.Information($"Open HomePage");
    }

    public void ClickOnStartmMenu(string menu)
    {
        browser.FindElement(repo.StartMenuByName(menu)).Click();
        log.Information($"Execute ClickOnStartItemMenu CommonAction mehod");
    }

    public void ClickOnElementsMenu() => ClickOnStartmMenu("Elements");
    public void ClickOnFormsMenu() => ClickOnStartmMenu("Forms");
    public void ClickOnAlertsFrameWindowsMenu() => ClickOnStartmMenu("Alerts, Frame & Windows");
    public void ClickOnWidgetsMenu() => ClickOnStartmMenu("Widgets");
    public void ClickOnInteractionsMenu() => ClickOnStartmMenu("Interactions");
    public void ClickOnBookStoreApplicationMenu() => ClickOnStartmMenu("Book Store Application");

}
