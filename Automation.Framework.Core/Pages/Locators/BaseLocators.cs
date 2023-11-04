namespace Automation.Framework.Core.Pages.Locators;

public class BaseLocators
{
    public string ButtonByName(string buttonName) =>
        $"//button[contains(text(),'{buttonName}')]";

    public string TextBoxFieldByName(string fieldName) =>
        $"//label[contains(text(),'{fieldName}')]/../following-sibling::*/*";

    public string MenuTitle => $"//div[@class='main-header']";
}
