using Automation.Framework.Common.Abstractions;
using Automation.Framework.Common.Models;
using Automation.Framework.Core.Configuration;
using Automation.Framework.Core.Pages.Components;
using Automation.Framework.Core.Pages.Locators;
using OpenQA.Selenium;

namespace Automation.Framework.Core.Pages;

public class ElementsPage : PageBase
{
    private ElementsLocators repo;
    private readonly LeftNavPanel leftNavPanel;
    private const string PAGE_NAME = "Elements Page";

    public LeftNavPanel LeftPanel => leftNavPanel;

    public ElementsPage(ElementsLocators repo, IWebDriverWrapper browser, ILogging log,
        TestRunConfiguration config, LeftNavPanel leftNavPanel)
        : base(browser, log, config)

    {
        this.repo = repo;
        this.leftNavPanel = leftNavPanel;
    }

    public void Open()
    {
        browser.NavigateToUrl($"{BaseUrl}/elements");
        log.Information($"|{PAGE_NAME}| {PAGE_NAME} is opened");
    }

    public string GetTitleOnMainPage()
    {
        var title = browser.FindElement(repo.MenuTitle).Text;
        log.Information($"|{PAGE_NAME}| Current title is '{title}'");

        return browser.FindElement(repo.MenuTitle).Text;
    }

    #region Text Box
    public void FillUserInfo(User user)
    {
        FillFullNameField(user.FullName);
        FillEmailField(user.Email);
        FillCurrentAddressField(user.CurrentAddress);
        FillPermanentAddressField(user.PermanentAddress);
    }

    public void FillFullNameField(string fullName)
    {
        browser.EnterText(repo.FullNameField, fullName);
        log.Information($"|{PAGE_NAME}| Full Name field is filled with - '{fullName}'");
    }

    public void FillEmailField(string email)
    {
        browser.EnterText(repo.EmailField, email);
        log.Information($"|{PAGE_NAME}| Email field is filled with - '{email}'");
    }

    public void FillCurrentAddressField(string curAddress)
    {
        browser.FindElement(repo.CurrentAddressField).SendKeys(curAddress);
        log.Information($"|{PAGE_NAME}| Current Address field is filled with - '{curAddress}'");
    }

    public void FillPermanentAddressField(string permAddress)
    {
        browser.FindElement(repo.PermanentAddressField).SendKeys(permAddress);
        log.Information($"|{PAGE_NAME}| Permanent Address field is filled with - '{permAddress}'");
    }

    public void ClickSubmitButton()
    {
        IWebElement submitButton = browser.FindElement(repo.SubmitButton);
        browser.ExecuteAsyncJSScriptForElement("arguments[0].scrollIntoView();", submitButton);
        submitButton.Click();
        log.Information($"|{PAGE_NAME}| Click on 'Subbmit' button");
    }

    public string GetOutputWindowText()
    {
        log.Information($"|{PAGE_NAME}| Getting output text");
        var outputText = browser.FindElement(repo.OutputMessage).Text;

        return outputText;
    }

    public bool isOutputWindowPresent()
    {
        log.Information($"|{PAGE_NAME}| Checking presence of output window");
        var outputField = browser.GetElementFromDOM(repo.OutputMessage);

        return outputField.Displayed;
    }

    #endregion
}
