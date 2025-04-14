
using BarterProject.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace BarterProject.Services.SignalR;

public class SignalRService(IHubContext<NotificationHub> hubContext) : ISignalRService
{
    private readonly IHubContext<NotificationHub> _hubContext = hubContext;

    public async Task SendMessageAsync(string message, int userId)
    {
        await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceiveNotification", message);
    }
}
