using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace Domain;

 public class Category{

        public int Id{get;set;}

        public string Name {get;set;}

        public virtual List<Product>? Products {get;set;}

    }

public class CategoryConfiguration:IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {


            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(20);
        }
    }