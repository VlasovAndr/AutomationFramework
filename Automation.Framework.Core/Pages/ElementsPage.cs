using Automation.Framework.Common.Abstractions;
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
        log.Information($"Open ElementsPage");
    }

    public string GetTitleOnMainPage()
    {
        log.Information($"Execute GetTitleOnMainPage CommonAction mehod");
        return browser.FindElement(repo.MenuTitle).Text;
    }

    #region Text Box

    public void FillFullNameField(string fullName)
    {
        browser.FindElement(repo.FullNameField).SendKeys(fullName);

        log.Information($"Execute FillFullNameField ElementsAction method");
    }

    public void FillEmailField(string email)
    {
        browser.EnterText(repo.EmailField, email);

        log.Information($"Execute FillEmailField ElementsAction method");
    }

    public void FillCurrentAddressField(string curAddress)
    {
        browser.FindElement(repo.CurrentAddressField).SendKeys(curAddress);

        log.Information($"Execute FillCurrentAddressField ElementsAction method");
    }

    public void PermanentAddressField(string permAddress)
    {
        browser.FindElement(repo.PermanentAddressField).SendKeys(permAddress);

        log.Information($"Execute PermanentAddressField ElementsAction method");
    }

    public void ClickSubbmitButton()
    {
        IWebElement submitButton = browser.FindElement(repo.SubmitButton);
        browser.ExecuteAsyncJSScriptForElement("arguments[0].scrollIntoView();", submitButton);
        submitButton.Click();

        log.Information($"Execute ClickSubbmitButton ElementsAction method");

    }

    public void ValidatOutputContainsValue(string expOutMessage)
    {
        log.Information($"Execute ValidatOutputContainsValue ElementsAction method");

        var actualOutMessage = browser.FindElement(repo.OutputMessage).Text;

        actualOutMessage.Should()
            .Contain(expOutMessage,
            $"Actual Output message - '{actualOutMessage}' does contain expected - '{expOutMessage}'");

        //Assert.IsTrue(isButtonVisible,
        //Assert.True(actualOutMessage.Contains(expOutMessage),
        //    $"Actual Output message - '{actualOutMessage}' does contain expected - '{expOutMessage}'");
    }

    public void ValidatEmptyOutput()
    {
        log.Information($"Execute ValidatEmptyOutput ElementsAction method");

        var outputField = browser.GetElementFromDOM(repo.OutputMessage);
        var isOutputEmpty = string.IsNullOrEmpty(outputField.Text) && !outputField.Displayed;

        isOutputEmpty
            .Should()
            .BeTrue($"Output message is not empty. Current value - '{outputField.Text}'");
        //Assert.IsTrue(isButtonVisible,
        //Assert.True(isOutputEmpty,
        //    $"Output message is not empty. Current value - '{outputField.Text}'");
    }

    #endregion
}
