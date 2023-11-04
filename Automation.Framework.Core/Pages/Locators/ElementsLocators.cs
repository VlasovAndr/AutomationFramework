namespace Automation.Framework.Core.Pages.Locators;

public class ElementsLocators : BaseLocators
{
    #region Text Box

    public string FullNameField => $"//input[@id = 'userName']";
    public string EmailField => $"//input[@id = 'userEmail']";
    public string CurrentAddressField => $"//textarea[@id = 'currentAddress']";
    public string PermanentAddressField => $"//textarea[@id = 'permanentAddress']";
    public string SubmitButton => $"//button[@id = 'submit']";
    public string OutputMessage => $"//div[@id = 'output']";

    #endregion
}
