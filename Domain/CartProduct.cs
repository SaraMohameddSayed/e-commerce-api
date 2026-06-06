using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CartProduct
    {
        public int id { get; set; }
        public int cartId { get; set; }
        public virtual Cart? cart { get; set; }
        public int productId { get; set; }
        public virtual Product? product { get; set; }
        public int quantity { get; set; }
    }
    public class cartProductConfiguration : IEntityTypeConfiguration<CartProduct>
    {
        public void Configure(EntityTypeBuilder<CartProduct> builder)
        {
            builder.HasKey(cp => cp.id);
            builder.HasOne(cp => cp.cart)
                   .WithMany(c => c.products)
                   .HasForeignKey(cp => cp.cartId);
            builder.HasOne(cp => cp.product)
                   .WithMany(p => p.carts)
                   .HasForeignKey(cp => cp.productId);
            builder.Property(cp => cp.quantity)
                   .IsRequired();
        }
    }
}
