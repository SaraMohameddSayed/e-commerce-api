using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ProductOffer
    {
        public int id { get; set; }
        public int productId { get; set; }
        public virtual Product? product { get; set; }
        public int offerId { get; set; }
        public virtual Offer? offer { get; set; }
        public decimal discountValue { get; set; }
        public DateTime applicationDate { get; set; }
    }
    public class ProductOfferConfiguration : IEntityTypeConfiguration<ProductOffer>
    {
        public void Configure(EntityTypeBuilder<ProductOffer> builder)
        {
            builder.HasKey(po => po.id);
            builder.HasOne(po => po.product)
                   .WithMany(p => p.offers)
                   .HasForeignKey(po => po.productId);
            builder.HasOne(po => po.offer)
                   .WithMany(o => o.products)
                   .HasForeignKey(po => po.offerId);
            builder.Property(po => po.discountValue)
                   .IsRequired();
            builder.Property(po => po.applicationDate)
                   .IsRequired();
        }
    }
}
