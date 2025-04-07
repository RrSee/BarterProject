using Serilog;

namespace BarterProject.Application.Services.Logging_Service;

public class LoggerService : ILoggerService
{
    public void LogError(string message, Exception ex)
    {
        Log.Error(ex, message);
    }

    public void LogInfo(string message)
    {
        Log.Information(message);
    }

    public void LogWarning(string message)
    {
        Log.Warning(message);
    }
}