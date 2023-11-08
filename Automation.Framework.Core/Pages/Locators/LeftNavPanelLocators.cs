namespace Automation.Framework.Core.Pages.Locators;

public class LeftNavPanelLocators
{
    public string LeftNavigationPanel => "//div[@class = 'left-pannel']";

    public string LeftMenuByName(string name) => $"{LeftNavigationPanel}//div[text() = '{name}']";

    public string LeftMenuContent(string name) =>
        $"{LeftMenuByName(name)}/ancestor::div[contains(@class,'element-group')]//div[contains(@class,'element-list collapse')]";
    
    public string ElementsSubMenuByName(string subMenuName) => $"{LeftNavigationPanel}//span[contains(text(),'{subMenuName}')]";
}
