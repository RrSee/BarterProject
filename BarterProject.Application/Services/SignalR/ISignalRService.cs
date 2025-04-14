namespace BarterProject.Services.SignalR;

public interface ISignalRService
{
    Task SendMessageAsync(string message, int userId);
}
