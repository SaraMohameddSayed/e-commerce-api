using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Domain
{
    public class Message
    {
        public int Id { get; set; }  
        public string Text { get; set; }
        public string? UserId { get; set; }    
        public string Email { get; set; }
    }

    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Text).IsRequired().HasMaxLength(500);
        }
    }
}
