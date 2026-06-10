using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace Domain;

 public class Cart
    {

        public int Id { get; set; }

        public string UserId { get; set; }

        public virtual IdentityUser User { get; set; }

        public virtual List<CartProduct>? Products { get; set; }


    }
public class CartConfiguration:IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {            
            builder.HasKey(c => c.Id);
            
        }
    }

