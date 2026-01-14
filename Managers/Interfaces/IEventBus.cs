using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managers.Interfaces
{
    public interface IEventBus
    {
        Task Publish<TEvent>(TEvent @event);
    }
}
