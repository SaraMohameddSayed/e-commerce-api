using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Domain
{
    public class Area
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal DeliveryFee { get; set; }
        public int GovernorateId { get; set; }
        public virtual Governorate Governorate { get; set; }
        public bool IsActive { get; set; } = true;
    }
    public class AreaConfiguration : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
        {


            builder.HasKey(c => c.Id);

        }
    }
}
