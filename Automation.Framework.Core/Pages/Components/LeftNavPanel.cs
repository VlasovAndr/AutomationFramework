using Automation.Framework.Common.Abstractions;
using Automation.Framework.Core.Pages.Locators;

namespace Automation.Framework.Core.Pages.Components;

public class LeftNavPanel
{
    private readonly IWebDriverWrapper browser;
    private readonly LeftNavPanelLocators repo;
    private readonly ILogging log;
    private const string COMPONENT_NAME = "Left Navigation Panel";

    public LeftNavPanel(IWebDriverWrapper browser, LeftNavPanelLocators repo, ILogging log)
    {
        this.browser = browser;
        this.repo = repo;
        this.log = log;
    }

    public void ExpandLeftPanelMenu(string menu)
    {
        ToggleLeftPanelMenu(menu, true);
    }

    public void CollapseLeftPanelMenu(string menu)
    {
        ToggleLeftPanelMenu(menu, false);
    }

    public void OpenSubMenu(string subMenuName)
    {
        var subMenu = browser.FindElement(repo.ElementsSubMenuByName(subMenuName), 20);
        browser.ExecuteAsyncJSScriptForElement("arguments[0].scrollIntoView();", subMenu);
        subMenu.Click();

        log.Information($"|{COMPONENT_NAME}| '{subMenuName}' submenu is opened ");
    }

    private void ToggleLeftPanelMenu(string menu, bool expand)
    {
        var isPanelOpened = browser.GetElementFromDOM(repo.LeftMenuContent(menu))
            .GetAttribute("className")
            .Contains("element-list collapse show");

        if ((expand && !isPanelOpened) || (!expand && isPanelOpened))
        {
            browser.FindElement(repo.LeftMenuByName(menu)).Click();
        }

        log.Information($"|{COMPONENT_NAME}| Left panel is {(expand ? "expanded" : "collapsed")} for '{menu}' menu");
    }
}
