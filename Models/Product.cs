using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
namespace Models;


public class Product
{

    public int id { get; set; }

    public string name { get; set; }

    public string imageUrl { get; set; }

    public decimal price { get; set; }

    public int quantity { get; set; }

    public string description { get; set; }

    public int categoryId { get; set; }

    public virtual Category category { get; set; }

    public virtual List<OrderProduct>? orders { get; set; }

    public virtual List<CartProduct>? carts { get; set; }

    public virtual List<ProductOffer>? offers { get; set; }


}

public class productConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.id);

        builder.Property(p => p.name).IsRequired();
        builder.Property(p => p.description).IsRequired();
        builder.Property(p => p.price).HasPrecision(5, 2).IsRequired();
        builder.Property(p => p.imageUrl).IsRequired();
        builder.HasOne(p => p.category).WithMany(c => c.products).HasForeignKey(p => p.categoryId);

    }
}