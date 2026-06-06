using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Domain;
namespace Infrastructure;

public class dbContext : IdentityDbContext<IdentityUser>
{

    public dbContext(DbContextOptions<dbContext> options) : base(options)
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

        modelBuilder.ApplyConfiguration(new cartConfiguration());
        modelBuilder.ApplyConfiguration(new cartProductConfiguration());
        modelBuilder.ApplyConfiguration(new categoryConfiguration());
        modelBuilder.ApplyConfiguration(new orderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderProductConfiguration());
        modelBuilder.ApplyConfiguration(new offerConfiguration());
        modelBuilder.ApplyConfiguration(new productConfiguration());
        modelBuilder.ApplyConfiguration(new ProductOfferConfiguration());
        modelBuilder.ApplyConfiguration(new messageConfiguration());
        modelBuilder.ApplyConfiguration(new GovernorateConfiguration());
        modelBuilder.ApplyConfiguration(new AreaConfiguration());
        modelBuilder.ApplyConfiguration(new NotificationConfiguration());


        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<IdentityRole>().HasData(
               new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" }

           );
        modelBuilder.Entity<Category>().HasData(
     new Category { id = 1, name = "المشروبات" },
     new Category { id = 2, name = "الأطعمة الأساسية" },
     new Category { id = 3, name = "المعلبات واللحوم" }
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
    // مشروبات
    new Product { id = 1, name = "قهوة عربية", description = "قهوة عربية تقليدية", price = 100, categoryId = 1,quantity=20, imageUrl = "https://example.com/images/arabic-coffee.jpg" },
    new Product { id = 2, name = "شاي أخضر", description = "شاي صحي ومنعش", price = 80, categoryId = 1,quantity=20, imageUrl = "https://example.com/images/green-tea.jpg" },
    new Product { id = 3, name = "عصير برتقال", description = "عصير طبيعي 100%", price = 50, categoryId = 1, quantity = 20, imageUrl = "https://example.com/images/orange-juice.jpg" },
    new Product { id = 4, name = "قهوة تركية", description = "قهوة تركية ممتازة", price = 120, categoryId = 1, quantity = 20, imageUrl = "https://example.com/images/turkish-coffee.jpg" },
    new Product { id = 5, name = "شاي أسود", description = "شاي أسود فاخر", price = 70, categoryId = 1, quantity = 20, imageUrl = "https://example.com/images/black-tea.jpg" },
    new Product { id = 6, name = "عصير تفاح", description = "عصير طبيعي منعش", price = 55, categoryId = 1, quantity = 20, imageUrl = "https://example.com/images/apple-juice.jpg" },

    // الأطعمة الأساسية
    new Product { id = 7, name = "جبنة بيضاء", description = "جبنة طازجة", price = 120, categoryId = 2, quantity = 20, imageUrl = "https://example.com/images/white-cheese.jpg" },
    new Product { id = 8, name = "بيض طازج", description = "بيض طازج يومي", price = 70, categoryId = 2, quantity = 20, imageUrl = "https://example.com/images/eggs.jpg" },
    new Product { id = 9, name = "سكر أبيض", description = "سكر ناعم", price = 40, categoryId = 2, quantity = 20, imageUrl = "https://example.com/images/sugar.jpg" },
    new Product { id = 10, name = "زيت ذرة", description = "زيت ذرة طبيعي", price = 90, categoryId = 2, quantity = 20, imageUrl = "https://example.com/images/corn-oil.jpg" },
    new Product { id = 11, name = "سمنة", description = "سمنة بلدي ممتازة", price = 100, categoryId = 2, quantity = 20, imageUrl = "https://example.com/images/ghee.jpg" },
    new Product { id = 12, name = "لبن كامل الدسم", description = "لبن طازج", price = 60, categoryId = 2, quantity = 20, imageUrl = "https://example.com/images/milk.jpg" },

    // معلبات ولحوم
    new Product { id = 13, name = "بسطرمة", description = "بسطرمة ممتازة", price = 200, categoryId = 3, quantity = 20, imageUrl = "https://example.com/images/pastrami.jpg" },
    new Product { id = 14, name = "تونة معلبة", description = "تونة طبيعية", price = 80, categoryId = 3, quantity = 20, imageUrl = "https://example.com/images/tuna.jpg" },
    new Product { id = 15, name = "فاصوليا معلبة", description = "فاصوليا طبيعية", price = 50, categoryId = 3, quantity = 20,   imageUrl = "https://example.com/images/beans.jpg" },

    // مزيد من المشروبات
    new Product { id = 16, name = "شاي أخضر بالنعناع", description = "شاي منعش صحي", price = 90, categoryId = 1 , quantity = 20, imageUrl = "https://example.com/images/mint-tea.jpg" },
    new Product { id = 17, name = "قهوة كابتشينو", description = "قهوة كابتشينو لذيذة", price = 140, categoryId = 1, quantity = 20, imageUrl = "https://example.com/images/cappuccino.jpg" },
    new Product { id = 18, name = "شاي ياسمين", description = "شاي فاخر برائحة الياسمين", price = 100, categoryId = 1, quantity = 20, imageUrl = "https://example.com/images/jasmine-tea.jpg" },
    new Product { id = 19, name = "عصير مانجو", description = "عصير طبيعي 100%", price = 60, categoryId = 1, quantity = 20, imageUrl = "https://example.com/images/mango-juice.jpg" },

    // مزيد من الأطعمة الأساسية
    new Product { id = 20, name = "جبنة رومي", description = "جبنة رومي ممتازة", price = 150, categoryId = 2, quantity = 20, imageUrl = "https://example.com/images/romi-cheese.jpg" },
    new Product { id = 21, name = "بيض أومليت", description = "بيض طازج للأومليت", price = 80, categoryId = 2, quantity = 20, imageUrl = "https://example.com/images/omelet-eggs.jpg" },
    new Product { id = 22, name = "سكر بني", description = "سكر بني طبيعي", price = 45, categoryId = 2, quantity = 20, imageUrl = "https://example.com/images/brown-sugar.jpg" },
    new Product { id = 23, name = "زيت زيتون", description = "زيت زيتون ممتاز", price = 150, categoryId = 2, quantity = 20, imageUrl = "https://example.com/images/olive-oil.jpg" },

    // مزيد من المعلبات
    new Product { id = 24, name = "بسطرمة مدخنة", description = "بسطرمة مدخنة فاخرة", price = 220, categoryId = 3, quantity = 20, imageUrl = "https://example.com/images/smoked-pastrami.jpg" },
    new Product { id = 25, name = "تونة صغيرة", description = "تونة طبيعية صغيرة", price = 70, categoryId = 3, quantity = 20, imageUrl = "https://example.com/images/tuna-small.jpg" },
    new Product { id = 26, name = "فاصوليا بيضاء", description = "فاصوليا طبيعية بيضاء", price = 55, categoryId = 3, quantity = 20, imageUrl = "https://example.com/images/white-beans.jpg" },

    // منتجات متنوعة إضافية
    new Product { id = 27, name = "عصير رمان", description = "عصير طبيعي منعش", price = 65, categoryId = 1, quantity = 20, imageUrl = "https://example.com/images/pomegranate-juice.jpg" },
    new Product { id = 28, name = "قهوة أمريكية", description = "قهوة أمريكية خفيفة", price = 130, categoryId = 1, quantity = 20, imageUrl = "https://example.com/images/american-coffee.jpg" },
    new Product { id = 29, name = "لبن زبادي", description = "لبن زبادي طازج", price = 40, categoryId = 2, quantity = 20, imageUrl = "https://example.com/images/yogurt.jpg" },
    new Product { id = 30, name = "سمنة بلدي", description = "سمنة بلدي ممتازة", price = 100, categoryId = 2, quantity = 20, imageUrl = "https://example.com/images/ghee2.jpg" }
);

        modelBuilder.Entity<ProductOffer>().HasData(
            new ProductOffer
            {
                id = 1,
                productId = 1,
                offerId = 1,
                discountValue = 10
            },
            new ProductOffer
            {
                id = 2,
                productId = 2,
                offerId = 1,
                discountValue = 20
            }
            );
    }
}