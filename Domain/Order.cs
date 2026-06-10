using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
namespace Domain;


public class Order
{
    public int Id { get; set; }

    //User
    public string UserId { get; set; }
    public virtual IdentityUser? User { get; set; }
   
    //Products
    public virtual List<OrderProduct>? Products { get; set; }
    
    //Status
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    //Address
    public string GovernorateName { get; set; }
    public string AreaName { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }

    //Payment
    public bool IsPaid { get; set; } = false;
    public PaymentMethod PaymentMethod { get; set; }  // cod / card

    //Pricing
    public decimal SubTotal { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal DeliveryFee { get; set; }
    
    //Meta
    public string? Notes { get; set; }
    public string TrackingNumber { get; set; }
    //Dates
    public DateTime CreatedAt { get; set; } 
    public DateTime UpdatedAt { get; set; } 

}


public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
       
    }
}