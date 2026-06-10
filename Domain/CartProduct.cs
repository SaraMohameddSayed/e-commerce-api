using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Domain
{
    public class CartProduct
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public virtual Cart? Cart { get; set; }
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public int Quantity { get; set; }
    }
    public class CartProductConfiguration : IEntityTypeConfiguration<CartProduct>
    {
        public void Configure(EntityTypeBuilder<CartProduct> builder)
        {
            builder.HasKey(cp => cp.Id);
            builder.HasOne(cp => cp.Cart)
                   .WithMany(c => c.Products)
                   .HasForeignKey(cp => cp.CartId);
            builder.HasOne(cp => cp.Product)
                   .WithMany(p => p.Carts)
                   .HasForeignKey(cp => cp.ProductId);
            builder.Property(cp => cp.Quantity)
                   .IsRequired();
        }
    }
}
