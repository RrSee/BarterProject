namespace BarterProject.Application.Services.Logging_Service;

public interface ILoggerService
{
    void LogInfo(string message);
    void LogWarning(string message);
    void LogError(string message, Exception ex);
}
