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
        log.Information($"Home page is opened");
    }

    public void ClickOnStartMenu(string menu)
    {
        browser.FindElement(repo.StartMenuByName(menu)).Click();
        log.Information($"'{menu}' start menu is opened");
    }

    public void ClickOnElementsMenu() => ClickOnStartMenu("Elements");
    public void ClickOnFormsMenu() => ClickOnStartMenu("Forms");
    public void ClickOnAlertsFrameWindowsMenu() => ClickOnStartMenu("Alerts, Frame & Windows");
    public void ClickOnWidgetsMenu() => ClickOnStartMenu("Widgets");
    public void ClickOnInteractionsMenu() => ClickOnStartMenu("Interactions");
    public void ClickOnBookStoreApplicationMenu() => ClickOnStartMenu("Book Store Application");

}
