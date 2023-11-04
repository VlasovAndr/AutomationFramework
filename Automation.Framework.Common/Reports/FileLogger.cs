using Automation.Framework.Common.Abstractions;
using Serilog;
using Serilog.Core;

namespace Automation.Framework.Common.Reports;

public class FileLogger : ILogging
{
    private LoggingLevelSwitch _loggingLevelSwitch;
    private IDefaultVariables _defaultVariables;

    public FileLogger(IDefaultVariables defaultVariables)
    {
        _defaultVariables = defaultVariables;
        _loggingLevelSwitch = new LoggingLevelSwitch(Serilog.Events.LogEventLevel.Debug);
        Log.Logger = new LoggerConfiguration().MinimumLevel.ControlledBy(_loggingLevelSwitch)
            .WriteTo.File(_defaultVariables.Log,
             outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .Enrich.WithThreadId().CreateLogger();

        Debug("Logger is FileLogger");
    }

    public void SetLogLevel(string loglevel)
    {
        switch (loglevel.ToLower())
        {
            case "debug":
                _loggingLevelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Debug;
                break;
            case "error":
                _loggingLevelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Error;
                break;
            case "information":
                _loggingLevelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Information;
                break;
            case "fatal":
                _loggingLevelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Fatal;
                break;
            default:
                _loggingLevelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Debug;
                break;
        }
    }

    public void Debug(string msg) => Log.Logger.Debug(msg);

    public void Error(string msg) => Log.Logger.Error(msg);

    public void Warning(string msg) => Log.Logger.Warning(msg);

    public void Information(string msg) => Log.Logger.Information(msg);

    public void Dispose() => Log.CloseAndFlush();
}
