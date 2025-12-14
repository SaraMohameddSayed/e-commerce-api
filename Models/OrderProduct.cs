using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class OrderProduct
    {
        public int id { get; set; } 
        public int orderId { get; set; }
        public virtual Order? order { get; set; }
        public int productId { get; set; }
        public virtual Product? product { get; set; }
        public decimal price { get; set; }  
        public int quantity { get; set; }

    }
    public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.HasKey(op => op.id);
            builder.HasOne(op => op.order)
                   .WithMany(o => o.products)
                   .HasForeignKey(op => op.orderId);
            builder.HasOne(op => op.product)
                   .WithMany(p => p.orders)
                   .HasForeignKey(op => op.productId);
            builder.Property(op => op.quantity)
                   .IsRequired();
        }
    }
}
