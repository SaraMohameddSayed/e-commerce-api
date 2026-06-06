using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IRealtimeNotifier
    {
        Task NotifyUserAsync(string userId,object payload,string signalREvent);
    }
}
