using Automation.Framework.Common.Params.Consts;
using Automation.Framework.Common.Services;
using Automation.Framework.Core.Pages;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using NUnit.Framework.Internal;

[assembly: LevelOfParallelism(3)]

namespace AutomationTestsPOM;

[TestFixture]
[Parallelizable(ParallelScope.All)]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[AllureNUnit]
[AllureSuite("Elements menu")]
public class ElementsMenuTests : TestBase
{
    private readonly ElementsPage elementsPage;

    public ElementsMenuTests()
    {
        elementsPage = container.GetRequiredService<ElementsPage>();
    }

    public static string[] MenuNames =
    {
        LeftSubMenu.TextBox,
        LeftSubMenu.CheckBox,
        LeftSubMenu.RadioButton,
        LeftSubMenu.WebTables,
        LeftSubMenu.Buttons,
        LeftSubMenu.Links,
        LeftSubMenu.BrokenLinksImages,
        LeftSubMenu.UploadAndDownload,
        LeftSubMenu.DynamicProperties
    };

    [TestCaseSource(nameof(MenuNames))]
    [Category("Elements_LeftPanel")]
    [AllureSubSuite("Elements_LeftPanel")]
    [Test, Property("TMSId", "C000001")]
    [Retry(3)]
    public void LeftPanelNavigationTest(string name)
    {
        elementsPage.Open();
        elementsPage.LeftPanel.OpenSubMenu(name);

        var title = elementsPage.GetTitleOnMainPage();
        title.Should().Be(name);
    }

    [Test, Property("TMSId", "C000002")]
    [Category("Elements_TextBox")]
    [AllureSubSuite("Elements_TextBox")]
    [Retry(2)]
    public void TextboxValidData()
    {
        var user = new DataGeneratorService().GenerateRandomUser();

        elementsPage.Open();
        elementsPage.LeftPanel.OpenSubMenu(LeftSubMenu.TextBox);
        elementsPage.FillUserInfo(user);
        elementsPage.ClickSubmitButton();

        var outputWindowState = elementsPage.IsOutputWindowPresent();
        outputWindowState.Should().BeTrue();
        var outputWindowText = elementsPage.GetOutputWindowText();
        outputWindowText.Should().Contain($"Name:{user.FullName}");
        outputWindowText.Should().Contain($"Email:{user.Email}");
        outputWindowText.Should().Contain($"Current Address :{user.CurrentAddress}");
        outputWindowText.Should().Contain($"Permananet Address :{user.PermanentAddress}");
    }

    [Test, Property("TMSId", "C000003")]
    [Category("Elements_TextBox")]
    [AllureSubSuite("Elements_TextBox")]
    [Retry(2)]
    public void TextboxEmptyField()
    {
        elementsPage.Open();
        elementsPage.LeftPanel.OpenSubMenu(LeftSubMenu.TextBox);
        elementsPage.ClickSubmitButton();

        var outputWindowText = elementsPage.GetOutputWindowText();
        outputWindowText.Should().BeEmpty();
    }

}