using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace Domain;

 public class Category{

        public int id{get;set;}

        public string name {get;set;}

        public virtual List<Product>? products {get;set;}


    }

public class categoryConfiguration:IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {


            builder.HasKey(c => c.id);

            builder.Property(c => c.name)
            .IsRequired()
            .HasMaxLength(20);
        }
    }