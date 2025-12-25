using Microsoft.AspNetCore.Identity;
using Models;
using System;
using System.Collections.Generic;
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
                customerId = order.userId,
                customerName=order.user.UserName,
                products = order.products?.Select(op => op.toViewModel()).ToList(),
                status = order.status,
                governorateName = order.governorateName,
                areaName = order.areaName,
                address = order.address,
                phone = order.phone,
                isPaid = order.isPaid,
                paymentMethod = order.paymentMethod,
                totalAmount = order.totalAmount,
                subTotal=order.subTotal,
                deliveryFee=order.delivaryFee,
                notes = order.notes,
                createdAt = order.createdAt,
                updatedAt = order.updatedAt,
                trackingNumber = order.trackingNumber
            };
        }

    }
}
