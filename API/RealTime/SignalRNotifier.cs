using API.Hubs;
using Application.Abstractions;
using Microsoft.AspNetCore.SignalR;

public class SignalRNotifier : IRealtimeNotifier
{
    private readonly IHubContext<NotificationHub> _hub;

    public SignalRNotifier(IHubContext<NotificationHub> hub)
    {
        _hub = hub;
    }

    public async Task NotifyUserAsync(string userId,object payload, string eventName)
    {
        await _hub.Clients
            .User(userId.ToString())
            .SendAsync(eventName, payload);
    }
}
