using Automation.Framework.Core.Pages;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using NUnit.Framework.Internal;

[assembly: LevelOfParallelism(5)]

namespace AutomationTestsPOM;

[TestFixture]
[Parallelizable(ParallelScope.All)]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
public class ElementsMenuTests : TestBase
{
    private readonly ElementsPage elementsPage;

    public ElementsMenuTests()
    {
        elementsPage = container.GetRequiredService<ElementsPage>();
    }

    public static string[] MenuName =
    {
        "Text Box",
        "Check Box",
        "Radio Button",
        "Web Tables",
        "Buttons",
        "Links",
        "Broken Links - Images",
        "Upload and Download",
        "Dynamic Properties"
    };

    [Parallelizable(ParallelScope.All)]
    [TestCaseSource(nameof(MenuName))]
    public void LeftPanelNavigationTest(string name)
    {
        elementsPage.Open();
        elementsPage.LeftPanel.OpenSubMenu(name);
        var title = elementsPage.GetTitleOnMainPage();
        title.Should().Be(name);
    }
}