﻿using Automation.Framework.Common.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Automation.Framework.Core.Configuration;

public class TestRunConfiguration
{
    public DriverConfig Driver { get; set; }
    public TargetEnvironment TargetEnvironment { get; set; }
    public FrameworkConfig Framework { get; set; }
    private ILogging log;
    private IDefaultVariables defaultVar;

    public TestRunConfiguration(ILogging log, IDefaultVariables defaultVar)
    {
        this.log = log;
        this.defaultVar = defaultVar;
        Configurate();
    }

    private void Configurate()
    {
        try
        {
            var configData = File.ReadAllText(defaultVar.Config);
            JObject data = JObject.Parse(configData);

            JToken driverSection = data["driver"];
            Driver = data["driver"].ToObject<DriverConfig>();
            Framework = data["framework"].ToObject<FrameworkConfig>();

            var envFromVariables = Environment.GetEnvironmentVariable("TARGET_ENVIRONMENT");
            var effectiveTargetEnv = string.IsNullOrEmpty(envFromVariables)
                                             ? Framework.Enviroment
                                             : envFromVariables;

            TargetEnvironment = data["enviroment"][effectiveTargetEnv.ToLower()].ToObject<TargetEnvironment>();

            log.Information($"Selected target environment: {effectiveTargetEnv}.");

        }
        catch (JsonException ex)
        {
            log.Error($"Error deserializing configuration: " + ex.Message);
            throw new ArgumentException("Error deserializing configuration: " + ex.Message, ex);

        }
        catch (Exception ex)
        {
            log.Error($"Error while configuration: " + ex.Message);
            throw new ArgumentException("Error while configuration: " + ex.Message, ex);
        }
    }
}
