
namespace Application.Abstractions
{
    public interface IRealtimeNotifier
    {
        Task NotifyUserAsync(string userId,object payload,string signalREvent);
    }
}
