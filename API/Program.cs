

using API.Hubs;
using Infrastructure;
using Infrastructure.Seeders;
using Services;
using Services.Abstractions;
using Services.EventBus;
using Services.Events;
using Services.Handlers;
using Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Domain;
using System;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddControllers();
//builder.Services.AddControllers().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
//});
builder.Services
.AddDbContext<dbContext>(options=>{
    options
    .UseSqlServer(builder.Configuration.GetConnectionString("TheConnection"))
    .UseLazyLoadingProxies();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") 
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

builder.Services
.AddIdentity<IdentityUser,IdentityRole>()
.AddEntityFrameworkStores<dbContext>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        //ValidIssuer = builder.Configuration["Jwtsettings:Issuer"],
        //ValidAudience = builder.Configuration["Jwtsettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwtsettings:Key"]))
    };
});

builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<cartProductManager>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<OfferService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<orderProductManager>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<productOfferManager>();
builder.Services.AddScoped<MessageService>();
builder.Services.AddScoped<CloudinaryService>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<GovernorateService>();
builder.Services.AddScoped<AreaService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<IEventBus, InMemoryEventBus>();
//Handlers
builder.Services.AddScoped<IEventHandler<OrderStatusChangedEvent>,
    OrderStatusChangedNotificationHandler>();
builder.Services.AddScoped<IEventHandler<OrderStatusChangedEvent>,
            OrderStatusChangedSignalRHandler>();
builder.Services.AddScoped<IEventHandler<NewOrderAddedEvent>,
            NewOrderAddedNotificationHandler>();
builder.Services.AddScoped<IEventHandler<NewOrderAddedEvent>,
            NewOrderAddedSignalRHandler>();
builder.Services.AddScoped<IEventHandler<NewProductAddedEvent>,
            NewProductAddedNotificationHandler>();
builder.Services.AddScoped<IEventHandler<NewProductAddedEvent>,
            NewProductAddedSignalRHandler>();
builder.Services.AddScoped<IEventHandler<NewOfferAddedEvent>,
            NewOfferAddedNotificationHandler>();
builder.Services.AddScoped<IEventHandler<NewOfferAddedEvent>,
            NewOfferAddedSignalRHandler>();
builder.Services.AddSignalR();
builder.Services.AddScoped<IRealtimeNotifier, SignalRNotifier>();
//
var app = builder.Build();
//seeding
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<dbContext>();
    await GovernorateAreaSeeder.SeedAsync(context);
    await AdminSeeder.SeedAsync(scope.ServiceProvider);
}

app.UseHttpsRedirection();
app.UseCors("AllowAngularDev");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<NotificationHub>("/hubs/notifications");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI();
}
app.UseStaticFiles();

app.Run();

