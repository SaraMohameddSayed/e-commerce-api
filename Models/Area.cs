using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Area
    {

        public int id { get; set; }
        public string name { get; set; }
        public decimal deliveryFee { get; set; }
        public int governorateId { get; set; }
        public virtual Governorate governorate { get; set; }
        public bool isActive { get; set; } = true;
    }
    public class AreaConfiguration : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
        {


            builder.HasKey(c => c.id);

        }
    }
}
