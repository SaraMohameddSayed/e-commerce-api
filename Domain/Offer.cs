using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace Domain;

 public class Offer
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public int Discount { get; set; }
    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; } 
        public virtual List<ProductOffer>? Products { get; set; }

}
public class OfferConfiguration:IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {            
            builder.HasKey(c => c.Id);
            builder.Property(o => o.Discount)
            .IsRequired();
            //builder.Property(o => o.StartDate)
            //.IsRequired();
            //builder.Property(o => o.EndDate)
            //.IsRequired();
            
        }
    }
