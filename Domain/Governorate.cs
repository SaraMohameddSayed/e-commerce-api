using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Domain
{
    public class Governorate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Area> Areas { get; set; }
        public bool IsActive { get; set; } = true;

    }
    public class GovernorateConfiguration : IEntityTypeConfiguration<Governorate>
    {
        public void Configure(EntityTypeBuilder<Governorate> builder)
        {


            builder.HasKey(c => c.Id);

        }
    }
}
