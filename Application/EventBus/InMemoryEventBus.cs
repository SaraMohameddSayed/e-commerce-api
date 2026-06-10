using Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;
namespace Application.EventBus
{
    public class InMemoryEventBus: IEventBus
    {
        public readonly IServiceProvider _serviceProvider;
        public InMemoryEventBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task Publish<TEvent>(TEvent @event)
        {
           var handlers= _serviceProvider.GetServices<IEventHandler<TEvent>>();
              foreach(var handler in handlers)
              {
                 await handler.Handle(@event);
            }
        }
    }
}
