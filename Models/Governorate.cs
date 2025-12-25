using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Governorate
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal deliveryFee { get; set; }
        public virtual ICollection<Area> areas { get; set; }
        public bool isActive { get; set; } = true;

    }
    public class GovernorateConfiguration : IEntityTypeConfiguration<Governorate>
    {
        public void Configure(EntityTypeBuilder<Governorate> builder)
        {


            builder.HasKey(c => c.id);

        }
    }
}
