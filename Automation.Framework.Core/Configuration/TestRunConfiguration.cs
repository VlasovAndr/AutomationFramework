using Automation.Framework.Common.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Automation.Framework.Core.Configuration;

public class TestRunConfiguration
{
    public DriverConfig Driver { get; set; }
    public TargetEnvironment TargetEnvironment { get; set; }
    public FrameworkConfig Framework { get; set; }
    private ILogging _log;
    private IDefaultVariables _defaultVariables;

    public TestRunConfiguration(ILogging log, IDefaultVariables defaultVariables)
    {
        _log = log;
        _defaultVariables = defaultVariables;
        Configurate();
    }

    private void Configurate()
    {
        try
        {
            var configData = File.ReadAllText(_defaultVariables.Config);
            JObject data = JObject.Parse(configData);

            JToken driverSection = data["driver"];
            Driver = data["driver"].ToObject<DriverConfig>();
            Framework = data["framework"].ToObject<FrameworkConfig>();

            var envFromVariables = Environment.GetEnvironmentVariable("TARGET_ENVIRONMENT");
            var effectiveTargetEnv = string.IsNullOrEmpty(envFromVariables)
                                             ? Framework.Enviroment
                                             : envFromVariables;

            TargetEnvironment = data["enviroment"][effectiveTargetEnv.ToLower()].ToObject<TargetEnvironment>();

            _log.Information($"Selected target environment: {effectiveTargetEnv}.");

        }
        catch (JsonException ex)
        {
            _log.Error($"Error deserializing configuration: " + ex.Message);
            throw new ArgumentException("Error deserializing configuration: " + ex.Message, ex);

        }
        catch (Exception ex)
        {
            _log.Error($"Error while configuration: " + ex.Message);
            throw new ArgumentException("Error while configuration: " + ex.Message, ex);
        }
    }
}
