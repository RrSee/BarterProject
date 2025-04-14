using Microsoft.AspNetCore.SignalR;

namespace BarterProject.SignalR;

public class NotificationHub : Hub
{
    public async Task SendNotificationToUser(int userId, string message)
    {
        await Clients.User(userId.ToString()).SendAsync("ReceiveNotification", message);
    }
}
