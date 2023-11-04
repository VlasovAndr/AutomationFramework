using Automation.Framework.Common.Abstractions;

namespace Automation.Framework.Core.Configuration;

public class TargetEnvironment
{
    public string Url { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}

public class DriverConfig
{
    public Browsers BrowserType { get; set; }
    public string GridHubUrl { get; set; }
    public int WaitSeconds { get; set; }
    public bool Headless { get; set; }
}

public class FrameworkConfig
{
    public string Enviroment { get; set; }
    public string DownloadedLocation { get; set; }
    public string StepScreenShot { get; set; }
    public string ExtentReportPortalUrl { get; set; }
    public string ExtentReportToPortal { get; set; }
    public string LogLevel { get; set; }
}
