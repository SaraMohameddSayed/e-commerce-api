using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Domain;
namespace Infrastructure;

public class AppDbContext  : IdentityDbContext <IdentityUser>
{

    public AppDbContext (DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public virtual DbSet<Product> Product { get; set; }
    public virtual DbSet<OrderProduct> OrderProduct { get; set; }
    public virtual DbSet<Offer> Offer { get; set; }
    public virtual DbSet<ProductOffer> ProductOffer { get; set; }
    public virtual DbSet<Order> Order { get; set; }
    public virtual DbSet<Category> Category { get; set; }
    public virtual DbSet<Cart> Cart { get; set; }
    public virtual DbSet<CartProduct> CartProduct { get; set; }
    public virtual DbSet<Message> Message { get; set; }
    public virtual DbSet<Governorate> Governorate { get; set; }
    public virtual DbSet<Area> Area { get; set; }
    public virtual DbSet<Notification> Notification { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfiguration(new CartConfiguration());
        modelBuilder.ApplyConfiguration(new CartProductConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderProductConfiguration());
        modelBuilder.ApplyConfiguration(new OfferConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new ProductOfferConfiguration());
        modelBuilder.ApplyConfiguration(new MessageConfiguration());
        modelBuilder.ApplyConfiguration(new GovernorateConfiguration());
        modelBuilder.ApplyConfiguration(new AreaConfiguration());
        modelBuilder.ApplyConfiguration(new NotificationConfiguration());


        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<IdentityRole>().HasData(
               new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" }

           );
        modelBuilder.Entity<Category>().HasData(
     new Category { Id = 1, Name = "المشروبات" },
     new Category { Id = 2, Name = "الأطعمة الأساسية" },
     new Category { Id = 3, Name = "المعلبات واللحوم" }
 );

        modelBuilder.Entity<Offer>().HasData(
            new Offer
            {
                Id = 1,
                Name = "خصم 10% على المشروبات",  
                Discount = 10
             
            }
            );
        modelBuilder.Entity<Product>().HasData(
    // مشروبات
    new Product { Id = 1, Name = "قهوة عربية", Description = "قهوة عربية تقليدية", Price = 100, CategoryId = 1, Quantity = 20, ImageUrl = "https://example.com/images/arabic-coffee.jpg" },
    new Product { Id = 2, Name = "شاي أخضر", Description = "شاي صحي ومنعش", Price = 80, CategoryId = 1, Quantity = 20, ImageUrl = "https://example.com/images/green-tea.jpg" },
    new Product { Id = 3, Name = "عصير برتقال", Description = "عصير طبيعي 100%", Price = 50, CategoryId = 1, Quantity = 20, ImageUrl = "https://example.com/images/orange-juice.jpg" },
    new Product { Id = 4, Name = "قهوة تركية", Description = "قهوة تركية ممتازة", Price = 120, CategoryId = 1, Quantity = 20, ImageUrl = "https://example.com/images/turkish-coffee.jpg" },
    new Product { Id = 5, Name = "شاي أسود", Description = "شاي أسود فاخر", Price = 70, CategoryId = 1, Quantity = 20, ImageUrl = "https://example.com/images/black-tea.jpg" },
    new Product { Id = 6, Name = "عصير تفاح", Description = "عصير طبيعي منعش", Price = 55, CategoryId = 1, Quantity = 20, ImageUrl = "https://example.com/images/apple-juice.jpg" },
    // الأطعمة الأساسية
    new Product { Id = 7, Name = "جبنة بيضاء", Description = "جبنة طازجة", Price = 120, CategoryId = 2, Quantity = 20, ImageUrl = "https://example.com/images/white-cheese.jpg" },
    new Product { Id = 8, Name = "بيض طازج", Description = "بيض طازج يومي", Price = 70, CategoryId = 2, Quantity = 20, ImageUrl = "https://example.com/images/eggs.jpg" },
    new Product { Id = 9, Name = "سكر أبيض", Description = "سكر ناعم", Price = 40, CategoryId = 2, Quantity = 20, ImageUrl = "https://example.com/images/sugar.jpg" },
    new Product { Id = 10, Name = "زيت ذرة", Description = "زيت ذرة طبيعي", Price = 90, CategoryId = 2, Quantity = 20, ImageUrl = "https://example.com/images/corn-oil.jpg" },
    new Product { Id = 11, Name = "سمنة", Description = "سمنة بلدي ممتازة", Price = 100, CategoryId = 2, Quantity = 20, ImageUrl = "https://example.com/images/ghee.jpg" },
    new Product { Id = 12, Name = "لبن كامل الدسم", Description = "لبن طازج", Price = 60, CategoryId = 2, Quantity = 20, ImageUrl = "https://example.com/images/milk.jpg" },

    // معلبات ولحوم
    new Product { Id = 13, Name = "بسطرمة", Description = "بسطرمة ممتازة", Price = 200, CategoryId = 3, Quantity = 20, ImageUrl = "https://example.com/images/pastrami.jpg" },
    new Product { Id = 14, Name = "تونة معلبة", Description = "تونة طبيعية", Price = 80, CategoryId = 3, Quantity = 20, ImageUrl = "https://example.com/images/tuna.jpg" },
    new Product { Id = 15, Name = "فاصوليا معلبة", Description = "فاصوليا طبيعية", Price = 50, CategoryId = 3, Quantity = 20,   ImageUrl = "https://example.com/images/beans.jpg" },

    // مزيد من المشروبات
    new Product { Id = 16, Name = "شاي أخضر بالنعناع", Description = "شاي منعش صحي", Price = 90, CategoryId = 1 , Quantity = 20, ImageUrl = "https://example.com/images/mint-tea.jpg" },
    new Product { Id = 17, Name = "قهوة كابتشينو", Description = "قهوة كابتشينو لذيذة", Price = 140, CategoryId = 1, Quantity = 20, ImageUrl = "https://example.com/images/cappuccino.jpg" },
    new Product { Id = 18, Name = "شاي ياسمين", Description = "شاي فاخر برائحة الياسمين", Price = 100, CategoryId = 1, Quantity = 20, ImageUrl = "https://example.com/images/jasmine-tea.jpg" },
    new Product { Id = 19, Name = "عصير مانجو", Description = "عصير طبيعي 100%", Price = 60, CategoryId = 1, Quantity = 20, ImageUrl = "https://example.com/images/mango-juice.jpg" },

    // مزيد من الأطعمة الأساسية
    new Product { Id = 20, Name = "جبنة رومي", Description = "جبنة رومي ممتازة", Price = 150, CategoryId = 2, Quantity = 20, ImageUrl = "https://example.com/images/romi-cheese.jpg" },
    new Product { Id = 21, Name = "بيض أومليت", Description = "بيض طازج للأومليت", Price = 80, CategoryId = 2, Quantity = 20, ImageUrl = "https://example.com/images/omelet-eggs.jpg" },
    new Product { Id = 22, Name = "سكر بني", Description = "سكر بني طبيعي", Price = 45, CategoryId = 2, Quantity = 20, ImageUrl = "https://example.com/images/brown-sugar.jpg" },
    new Product { Id = 23, Name = "زيت زيتون", Description = "زيت زيتون ممتاز", Price = 150, CategoryId = 2, Quantity = 20, ImageUrl = "https://example.com/images/olive-oil.jpg" },

    // مزيد من المعلبات
    new Product { Id = 24, Name = "بسطرمة مدخنة", Description = "بسطرمة مدخنة فاخرة", Price = 220, CategoryId = 3, Quantity = 20, ImageUrl = "https://example.com/images/smoked-pastrami.jpg" },
    new Product { Id = 25, Name = "تونة صغيرة", Description = "تونة طبيعية صغيرة", Price = 70, CategoryId = 3, Quantity = 20, ImageUrl = "https://example.com/images/tuna-small.jpg" },
    new Product { Id = 26, Name = "فاصوليا بيضاء", Description = "فاصوليا طبيعية بيضاء", Price = 55, CategoryId = 3, Quantity = 20, ImageUrl = "https://example.com/images/white-beans.jpg" },

    // منتجات متنوعة إضافية
    new Product { Id = 27, Name = "عصير رمان", Description = "عصير طبيعي منعش", Price = 65, CategoryId = 1, Quantity = 20, ImageUrl = "https://example.com/images/pomegranate-juice.jpg" },
    new Product { Id = 28, Name = "قهوة أمريكية", Description = "قهوة أمريكية خفيفة", Price = 130, CategoryId = 1, Quantity = 20, ImageUrl = "https://example.com/images/american-coffee.jpg" },
    new Product { Id = 29, Name = "لبن زبادي", Description = "لبن زبادي طازج", Price = 40, CategoryId = 2, Quantity = 20, ImageUrl = "https://example.com/images/yogurt.jpg" },
    new Product { Id = 30, Name = "سمنة بلدي", Description = "سمنة بلدي ممتازة", Price = 100, CategoryId = 2, Quantity = 20, ImageUrl = "https://example.com/images/ghee2.jpg" }
);

        modelBuilder.Entity<ProductOffer>().HasData(
            new ProductOffer
            {
                Id = 1,
                ProductId = 1,
                OfferId = 1,
                DiscountValue = 10
            },
            new ProductOffer
            {
                Id = 2,
                ProductId = 2,
                OfferId = 1,
                DiscountValue = 20
            }
            );
    }
}