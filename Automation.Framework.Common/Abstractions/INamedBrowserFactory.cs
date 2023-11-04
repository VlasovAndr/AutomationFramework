using Automation.Framework.Common.Abstractions;
using OpenQA.Selenium;

namespace Automation.Framework.Core.WebUI.Selenium;

public interface INamedBrowserFactory : IFactory<IWebDriver>
{
    Browsers Name { get; }
}

public interface IFactory<out T>
{
    T Create();
}
