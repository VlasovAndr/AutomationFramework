using Automation.Framework.Common.Abstractions;
using OpenQA.Selenium;

namespace Automation.Framework.Core.WebUI.Selenium;

public interface INamedBrowserFactory : IFactory<IWebDriver>
{
    BrowserName Name { get; }
    BrowserType Type { get; }
}

public interface IFactory<out T>
{
    T Create();
}
