using Automation.Framework.Common.Abstractions;
using Automation.Framework.Common.Models;
using Automation.Framework.Core.Configuration;
using Automation.Framework.Core.Pages.Components;
using Automation.Framework.Core.Pages.Locators;
using FluentAssertions;
using OpenQA.Selenium;

namespace Automation.Framework.Core.Pages;

public class ElementsPage : PageBase
{
    private ElementsLocators repo;
    private readonly LeftNavPanel leftNavPanel;

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
        log.Information($"Elements page is opened");
    }

    public string GetTitleOnMainPage()
    {
        var title = browser.FindElement(repo.MenuTitle).Text;
        log.Information($"Current title is '{title}'");

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
        log.Information($"Full Name field is filled with - '{fullName}'");
    }

    public void FillEmailField(string email)
    {
        browser.EnterText(repo.EmailField, email);
        log.Information($"Email field is filled with - '{email}'");
    }

    public void FillCurrentAddressField(string curAddress)
    {
        browser.FindElement(repo.CurrentAddressField).SendKeys(curAddress);
        log.Information($"Current Address field is filled with - '{curAddress}'");
    }

    public void FillPermanentAddressField(string permAddress)
    {
        browser.FindElement(repo.PermanentAddressField).SendKeys(permAddress);
        log.Information($"PermanentAddress field is filled with - '{permAddress}'");
    }

    public void ClickSubmitButton()
    {
        IWebElement submitButton = browser.FindElement(repo.SubmitButton);
        browser.ExecuteAsyncJSScriptForElement("arguments[0].scrollIntoView();", submitButton);
        submitButton.Click();
        log.Information($"Click on 'Subbmit' button");
    }

    public string GetOutputWindowText()
    {
        log.Information($"Getting output text");
        var outputText = browser.FindElement(repo.OutputMessage).Text;

        return outputText;
    }

    public void ValidatOutputContainsValue(string expOutMessage)
    {
        log.Information($"Validating output text");
        var actualOutMessage = browser.FindElement(repo.OutputMessage).Text;

        actualOutMessage.Should()
            .Contain(expOutMessage,
            $"Actual Output message - '{actualOutMessage}' does contain expected - '{expOutMessage}'");
    }

    public bool isOutputWindowPresent()
    {
        log.Information($"Checking presence of output window");
        var outputField = browser.GetElementFromDOM(repo.OutputMessage);

        return outputField.Displayed;
    }

    public void ValidatEmptyOutput()
    {
        log.Information($"Checking that output window is empty");
        var outputField = browser.GetElementFromDOM(repo.OutputMessage);
        var isOutputEmpty = string.IsNullOrEmpty(outputField.Text) && !outputField.Displayed;

        isOutputEmpty
            .Should()
            .BeTrue($"Output message is not empty. Current value - '{outputField.Text}'");
    }

    #endregion
}
