using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace Domain;


public class Product
{

    public int Id { get; set; }

    public string Name { get; set; }

    public string ImageUrl { get; set; }
    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public string Description { get; set; }

    public int CategoryId { get; set; }
    public bool IsActive { get; set; } = true;

    public virtual Category Category { get; set; }

    public virtual List<OrderProduct>? Orders { get; set; }
    public virtual List<CartProduct>? Carts { get; set; }

    public virtual List<ProductOffer>? Offers { get; set; }

}

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);  
        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.Description).IsRequired();
        builder.Property(p => p.Price).HasPrecision(5, 2).IsRequired();
        builder.Property(p => p.ImageUrl).IsRequired();
        builder.HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId);
    }
}