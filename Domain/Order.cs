using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
namespace Domain;


public class Order
{
    public int id { get; set; }

    //User
    public string userId { get; set; }
    public virtual IdentityUser? user { get; set; }
   
    //Products
    public virtual List<OrderProduct>? products { get; set; }
    
    //Status
    public OrderStatus status { get; set; } = OrderStatus.Pending;

    //Address
    public string governorateName { get; set; }
    public string areaName { get; set; }
    public string address { get; set; }
    public string phone { get; set; }

    //Payment
    public bool isPaid { get; set; } = false;
    public PaymentMethod paymentMethod { get; set; }  // cod / card

    //Pricing
    public decimal subTotal { get; set; }
    public decimal totalAmount { get; set; }
    public decimal delivaryFee { get; set; }
    
    //Meta
    public string? notes { get; set; }
    public string trackingNumber { get; set; }

    //Dates
    public DateTime createdAt { get; set; } 
    public DateTime updatedAt { get; set; } 

}


public class orderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.id);
       
    }
}