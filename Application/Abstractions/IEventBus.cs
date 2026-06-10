
namespace Application.Abstractions
{
    public interface IEventBus
    {
        Task Publish<TEvent>(TEvent @event);
    }
}
