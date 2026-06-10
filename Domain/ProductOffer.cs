using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace Domain
{
    public class ProductOffer
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public int OfferId { get; set; }
        public virtual Offer? Offer { get; set; }
        public decimal DiscountValue { get; set; }
        public DateTime ApplicationDate { get; set; }
    }
    public class ProductOfferConfiguration : IEntityTypeConfiguration<ProductOffer>
    {
        public void Configure(EntityTypeBuilder<ProductOffer> builder)
        {
            builder.HasKey(po => po.Id);
            builder.HasOne(po => po.Product)
                   .WithMany(p => p.Offers)
                   .HasForeignKey(po => po.ProductId);
            builder.HasOne(po => po.Offer)
                   .WithMany(o => o.Products)
                   .HasForeignKey(po => po.OfferId);
            builder.Property(po => po.DiscountValue)
                   .IsRequired();
            builder.Property(po => po.ApplicationDate)
                   .IsRequired();
        }
    }
}
