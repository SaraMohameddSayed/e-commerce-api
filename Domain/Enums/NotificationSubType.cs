using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum NotificationSubType
    {
        None=0,
        //Order=User
        OrderStatusChanged = 1,
        PaymentFailed = 2,

        //System=Admin
        NewOrder = 3,
        LowStock = 4,

        //Promo
        NewOffer = 5,
        NewProduct = 6

    }
}
