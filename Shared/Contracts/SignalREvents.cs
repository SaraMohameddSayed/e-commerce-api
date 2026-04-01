using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Contracts
{
    public static class SignalREvents
    {
        public const string OrderStatusUpdated = "order-status-updated";
        public const string NewOrderAdded = "new-order-added";
        public const string NewProductAdded = "new-product-added";
        public const string NewOfferAdded = "new-offer-added";
    }
}
