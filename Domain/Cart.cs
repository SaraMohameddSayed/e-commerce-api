using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace Domain;

 public class Cart
    {

        public int id { get; set; }

        public string userId { get; set; }

        public virtual IdentityUser user { get; set; }

        public virtual List<CartProduct>? products { get; set; }


    }
public class cartConfiguration:IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {            
            builder.HasKey(c => c.id);
            
        }
    }

