using Microsoft.AspNetCore.Identity;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DTOs
{
    public static class OrderExtensions
    {
        public static OrderResponse ToResponse(this Order order)
        {

            return new OrderResponse
            {
                Id = order.Id,
                CustomerId = order.UserId,
                CustomerName = order.User.UserName,
                Products = order.Products?.Select(op => op.ToResponse()).ToList(),
                Status = order.Status,
                GovernorateName = order.GovernorateName,
                AreaName = order.AreaName,
                Address = order.Address,
                Phone = order.Phone,
                IsPaid = order.IsPaid,
                PaymentMethod = order.PaymentMethod,
                TotalAmount = order.TotalAmount,
                SubTotal = order.SubTotal,
                DeliveryFee = order.DeliveryFee,
                Notes = order.Notes,
                CreatedAt = order.CreatedAt,
                UpdatedAt = order.UpdatedAt,
                TrackingNumber = order.TrackingNumber
            };
        }

    }
}
