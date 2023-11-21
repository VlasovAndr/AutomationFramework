using Automation.Framework.Common.Abstractions;
using Automation.Framework.Common.Models;
using Automation.Framework.Core.Configuration;
using Automation.Framework.Core.Pages.Components;
using Automation.Framework.Core.Pages.Locators;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;

namespace Automation.Framework.Core.Pages;

public class ElementsPage : PageBase
{
    private ElementsLocators repo;
    private readonly LeftNavPanel leftNavPanel;
    private const string PAGE_NAME = "Elements Page";

    private string PageUrl => $"{BaseUrl}/elements";
    public LeftNavPanel LeftPanel => leftNavPanel;

    public ElementsPage(ElementsLocators repo, IWebDriverWrapper browser, ILogging log,
        TestRunConfiguration config, LeftNavPanel leftNavPanel)
        : base(browser, log, config)

    {
        this.repo = repo;
        this.leftNavPanel = leftNavPanel;
    }

    [AllureStep($"Open {PAGE_NAME}")]
    public void Open()
    {
        browser.NavigateToUrl(PageUrl);
        log.Information($"|{PAGE_NAME}| {PAGE_NAME} is opened");
    }

    [AllureStep($"Geting title on {PAGE_NAME}")]
    public string GetTitleOnMainPage()
    {
        var title = browser.FindElement(repo.MenuTitle).Text;
        log.Information($"|{PAGE_NAME}| Current title is '{title}'");

        return browser.FindElement(repo.MenuTitle).Text;
    }

    #region Text Box
    [AllureStep("Fill user info fields")]
    public void FillUserInfo(User user)
    {
        FillFullNameField(user.FullName);
        FillEmailField(user.Email);
        FillCurrentAddressField(user.CurrentAddress);
        FillPermanentAddressField(user.PermanentAddress);
    }

    [AllureStep("Fill full name field")]
    public void FillFullNameField(string fullName)
    {
        browser.EnterText(repo.FullNameField, fullName);
        log.Information($"|{PAGE_NAME}| Full Name field is filled with - '{fullName}'");
    }

    [AllureStep("Fill email field")]
    public void FillEmailField(string email)
    {
        browser.EnterText(repo.EmailField, email);
        log.Information($"|{PAGE_NAME}| Email field is filled with - '{email}'");
    }

    [AllureStep("Fill currenta address field")]
    public void FillCurrentAddressField(string curAddress)
    {
        browser.FindElement(repo.CurrentAddressField).SendKeys(curAddress);
        log.Information($"|{PAGE_NAME}| Current Address field is filled with - '{curAddress}'");
    }

    [AllureStep("Fill permanent address field")]
    public void FillPermanentAddressField(string permAddress)
    {
        browser.FindElement(repo.PermanentAddressField).SendKeys(permAddress);
        log.Information($"|{PAGE_NAME}| Permanent Address field is filled with - '{permAddress}'");
    }

    [AllureStep("Click on submit button")]
    public void ClickSubmitButton()
    {
        IWebElement submitButton = browser.FindElement(repo.SubmitButton);
        browser.ExecuteAsyncJSScriptForElement("arguments[0].scrollIntoView();", submitButton);
        submitButton.Click();
        log.Information($"|{PAGE_NAME}| Click on 'Subbmit' button");
    }

    [AllureStep("Getting output text")]
    public string GetOutputWindowText()
    {
        log.Information($"|{PAGE_NAME}| Getting output text");
        var outputText = browser.FindElement(repo.OutputMessage).Text;

        return outputText;
    }

    [AllureStep("Getting output window status")]
    public bool IsOutputWindowPresent()
    {
        log.Information($"|{PAGE_NAME}| Checking presence of output window");
        var outputField = browser.GetElementFromDOM(repo.OutputMessage);

        return outputField.Displayed;
    }

    #endregion
}
