using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace Domain;

 public class Offer
    {

        public int id { get; set; }

        public string name { get; set; }

        public int discount { get; set; }

    public DateTime? startDate { get; set; }

    public DateTime? endDate { get; set; } 
        public virtual List<ProductOffer>? products { get; set; }


}
public class offerConfiguration:IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {            
            builder.HasKey(c => c.id);

            builder.Property(o => o.discount)
            .IsRequired();
            //builder.Property(o => o.startDate)
            //.IsRequired();
            //builder.Property(o => o.endDate)
            //.IsRequired();
            
        }
    }
