using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeders
{
    public static class GovernorateAreaSeeder
    {
        public static async Task SeedAsync(AppDbContext  _context)
        {
            if (_context.Governorate.Any())
                return;

            var governorates = new List<Governorate>
        {
            new Governorate
            {
                Name = "القاهرة",
                Areas = new List<Area>
                {
                    new Area { Name = "مدينة نصر" ,IsActive=true,DeliveryFee=15},
                    new Area { Name = "المعادي" ,IsActive=true,DeliveryFee=20},
                    new Area { Name = "الزمالك" ,IsActive=true,DeliveryFee=25},
                    new Area { Name = "حلوان" , IsActive=true,DeliveryFee=30}
                },
                IsActive=true
            },
            new Governorate
            {
                Name = "الجيزة",
                Areas = new List<Area>
                {
                    new Area { Name = "الدقي", IsActive=true, DeliveryFee=35 },
                    new Area { Name = "المهندسين" , IsActive = true, DeliveryFee=40},
                    new Area { Name = "6 أكتوبر" , IsActive = true, DeliveryFee=45},
                    new Area { Name = "الشيخ زايد" , IsActive = true, DeliveryFee=50}
                },
                IsActive=true
            },
            new Governorate
            {
               Name = "الإسكندرية",
               Areas = new List<Area>
               {
                   new Area { Name = "سيدي جابر" ,IsActive=true,DeliveryFee=55},
                   new Area { Name = "محرم بك" ,IsActive=true ,DeliveryFee=60},
                   new Area { Name = "العجمي" ,IsActive=true ,DeliveryFee=70}
                },
                IsActive=true
             },
            new Governorate
            {
                Name = "المنصورة",
                Areas = new List<Area>
                {
                    new Area { Name = "المنصورة الجديدة", IsActive=true ,DeliveryFee=75},
                    new Area { Name = "ميت غمر" , IsActive=true ,DeliveryFee=80}
                },
                IsActive=true
            }
           };
            await _context.Governorate.AddRangeAsync(governorates);
            await _context.SaveChangesAsync();
        }
    }

    }
