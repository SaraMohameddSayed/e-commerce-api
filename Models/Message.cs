using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Message
    {
        public int id { get; set; }  
        public string text { get; set; }
        public string? userId { get; set; }    
        public string email { get; set; }
    }

    public class messageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(m => m.id);
            builder.Property(m => m.text).IsRequired().HasMaxLength(500);
        }
    }
}
