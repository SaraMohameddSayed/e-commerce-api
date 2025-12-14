using System;
using System.Collections.Generic;using Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public static class orderExtensions
    {
        public static orderViewModel toViewModel(this Order order)
        {
            return new orderViewModel
            {
                id = order.id,
                userId = order.userId,
                products = order.products?.Select(op => op.toViewModel()).ToList(),
                status = order.status,
                country = order.country,
                city = order.city,
                address = order.address,
                phone = order.phone,
                isPaid = order.isPaid,
                paymentMethod = order.paymentMethod,
                totalAmount = order.totalAmount,
                notes = order.notes,
                createdAt = order.createdAt,
                updatedAt = order.updatedAt,
                trackingNumber = order.trackingNumber
            };
        }

    }
}
