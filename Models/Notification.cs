using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Models
{
    public class Notification
    {
        public int Id { get; set; }

        // Nullable → Admin Notification
        public string? UserId { get; set; }
        public virtual IdentityUser? User { get; set; }

        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!;

        public NotificationType Type { get; set; }
        public NotificationSubType SubType { get; set; }

        // Polymorphic reference → for deep linking with enities
        public string? EntityType { get; set; }   // Order, Product, Offer
        public long? EntityId { get; set; }

        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    };
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification> { 

        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.Property(n => n.Title).IsRequired().HasMaxLength(200);
            builder.Property(n => n.Message).IsRequired().HasMaxLength(500);
            builder.HasIndex(n => n.UserId);
            builder.HasIndex(n => n.Type);
            builder.HasIndex(n => n.IsRead);
            builder.HasIndex(n => n.CreatedAt);
            builder.HasOne(n=>n.User).WithMany()
                   .HasForeignKey(n => n.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

        }



    }
}
