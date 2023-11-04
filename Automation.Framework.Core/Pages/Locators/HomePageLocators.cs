namespace Automation.Framework.Core.Pages.Locators;

public class HomePageLocators : BaseLocators
{
    public string StartNavigationPanel => "//div[@class = 'category-cards']";
    public string StartMenuByName(string name) =>
        $"{StartNavigationPanel}//h5[contains(text(),'{name}')]/../../..";

    public string TextBoxFieldByName(string fieldName) =>
        $"//label[contains(text(),'{fieldName}')]/../following-sibling::*/*";
    public string ButtonByName(string buttonName) => $"//button[contains(text(),'{buttonName}')]";
}
