

namespace Application.Abstractions
{
    public interface IEventHandler<TEvent>
    {
        Task Handle(TEvent @event);
    }
}
