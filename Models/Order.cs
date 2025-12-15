using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Models.Enums;
namespace Models;


public class Order
{
    public int id { get; set; }

    public string userId { get; set; }

    public virtual List<OrderProduct>? products { get; set; }

    public OrderStatus status { get; set; } = OrderStatus.Pending;

    public string country { get; set; }
    public string city { get; set; }
    public string address { get; set; }
    public string phone { get; set; }

    public bool isPaid { get; set; } = false;
    public string paymentMethod { get; set; }  // cod / card

    public decimal totalAmount { get; set; }

    public string? notes { get; set; }

    public DateTime createdAt { get; set; } 
    public DateTime updatedAt { get; set; } 
    public string trackingNumber { get; set; } 

}


public class orderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.id);
       
    }
}