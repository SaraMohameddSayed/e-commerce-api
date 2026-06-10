using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace Domain
{
    public class OrderProduct
    {
        public int Id { get; set; } 
        public int OrderId { get; set; }
        public virtual Order? Order { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImageUrl { get; set; }

        public virtual Product? Product { get; set; }
        public decimal Price { get; set; }  
        public int Quantity { get; set; }
    }
    public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.HasKey(op => op.Id);
            builder.HasOne(op => op.Order)
                   .WithMany(o => o.Products)
                   .HasForeignKey(op => op.OrderId);
            builder.HasOne(op => op.Product)
                   .WithMany(p => p.Orders)
                   .HasForeignKey(op => op.ProductId);
            builder.Property(op => op.Quantity)
                   .IsRequired();
        }
    }
}
