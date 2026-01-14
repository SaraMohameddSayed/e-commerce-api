

using Infrastructure;
using Infrastructure.Seeders;
using Managers;
using Managers.EventBus;
using Managers.Events;
using Managers.Interfaces;
using Managers.Handlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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

builder.Services.AddCors(i => i.AddDefaultPolicy(
    i => i.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()));
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

builder.Services.AddScoped<cartManager>();
builder.Services.AddScoped<cartProductManager>();
builder.Services.AddScoped<categoryManager>();
builder.Services.AddScoped<offerManager>();
builder.Services.AddScoped<orderManager>();
builder.Services.AddScoped<orderProductManager>();
builder.Services.AddScoped<productManager>();
builder.Services.AddScoped<productOfferManager>();
builder.Services.AddScoped<messageManager>();
builder.Services.AddScoped<cloudinaryManager>();
builder.Services.AddScoped<accountManager>();
builder.Services.AddScoped<tokenManager>();
builder.Services.AddScoped<governorateManager>();
builder.Services.AddScoped<areaManager>();
builder.Services.AddScoped<IEventBus, InMemoryEventBus>();

builder.Services.AddScoped<IEventHandler<OrderStatusChangedEvent>,
    OrderStatusChangedNotificationHandler>();


var app = builder.Build();
//seeding
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<dbContext>();
    await GovernorateAreaSeeder.SeedAsync(context);
}
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI();
}
app.UseStaticFiles();



// ‘€¯·Ì «ŸÂ«— «·‹ PII
IdentityModelEventSource.ShowPII = true;
// ·Ê ⁄«Ì“… ﬂ„«‰ ÌŸÂ— «· Êﬂ‰ ﬂ«„·« ›Ì «··ÊÃ“
IdentityModelEventSource.LogCompleteSecurityArtifact = true;

app.Run();

