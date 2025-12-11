using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Models;
namespace Infrastructure;

public class dbContext : IdentityDbContext<IdentityUser>
{

    public dbContext(DbContextOptions<dbContext> options) : base(options)
    {

    }
    public virtual DbSet<Product> Product { get; set; }
    public virtual DbSet<Offer> Offer { get; set; }
    public virtual DbSet<ProductOffer> ProductOffer { get; set; }
    public virtual DbSet<Order> Order { get; set; }
    public virtual DbSet<Category> Category { get; set; }
    public virtual DbSet<Cart> Cart { get; set; }
    public virtual DbSet<CartProduct> cartProduct { get; set; }
    public virtual DbSet<Message> Message { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfiguration(new cartConfiguration());
        modelBuilder.ApplyConfiguration(new cartProductConfiguration());
        modelBuilder.ApplyConfiguration(new categoryConfiguration());
        modelBuilder.ApplyConfiguration(new orderConfiguration());
        modelBuilder.ApplyConfiguration(new offerConfiguration());
        modelBuilder.ApplyConfiguration(new productConfiguration());
        modelBuilder.ApplyConfiguration(new ProductOfferConfiguration());
        modelBuilder.ApplyConfiguration(new messageConfiguration());

        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<IdentityRole>().HasData(
               new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" }

           );
        modelBuilder.Entity<Category>().HasData(
            new Category { id = 1, name = "المشروبات" }
            );

        modelBuilder.Entity<Offer>().HasData(
            new Offer
            {
                id = 1,
                name = "خصم 10% على المشروبات",  
                discount = 10
             
            }
            );
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                id = 1,
                name = "قهوة عربية",
                description = "قهوة عربية تقليدية",
                price = 100,
                categoryId = 1,
                imageUrl = "https://example.com/images/arabic-coffee.jpg"
            },
            new Product
            {
                id = 2,
                name = "شاي أخضر",
                description = "شاي أخضر صحي ومنعش",
                price = 200,
                categoryId = 1,
                imageUrl = "https://example.com/images/green-tea.jpg"
            }
            );
        modelBuilder.Entity<ProductOffer>().HasData(
            new ProductOffer
            {
                id = 1,
                productId = 1,
                offerId = 1
            },
            new ProductOffer
            {
                id = 2,
                productId = 2,
                offerId = 1
            }
            );
    }
}