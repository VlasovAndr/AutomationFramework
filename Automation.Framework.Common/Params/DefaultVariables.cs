using Automation.Framework.Common.Abstractions;

namespace Automation.Framework.Common.Params;

public class DefaultVariables : IDefaultVariables
{
    private readonly string _baseDirectory;

    public DefaultVariables()
    {
        _baseDirectory = Directory.GetParent($"{AppDomain.CurrentDomain.BaseDirectory}../../../").FullName;
    }

    public string Results =>
        Path.Combine(_baseDirectory, "Results", "Reports", DateTime.Now.ToString("yyyyMMdd HHmmss"));

    public string Log
        => Path.Combine(Results, "log.txt");

    public string Solution
        => Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;

    public string Config
        => Path.Combine(Solution, "Automation.Framework.Core", "Configuration", "Resources", "test-settings.json");
}
