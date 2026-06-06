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
        public static async Task SeedAsync(dbContext _context)
        {
            if (_context.Governorate.Any())
                return;

            var governorates = new List<Governorate>
        {
            new Governorate
            {
                name = "القاهرة",
                areas = new List<Area>
                {
                    new Area { name = "مدينة نصر" ,isActive=true,deliveryFee=15},
                    new Area { name = "المعادي" ,isActive=true,deliveryFee=20},
                    new Area { name = "الزمالك" ,isActive=true,deliveryFee=25},
                    new Area { name = "حلوان" , isActive=true,deliveryFee=30}
                },
                isActive=true
            },
            new Governorate
            {
                name = "الجيزة",
                areas = new List<Area>
                {
                    new Area { name = "الدقي",isActive=true,deliveryFee=35 },
                    new Area { name = "المهندسين" , isActive = true,deliveryFee=40},
                    new Area { name = "6 أكتوبر" , isActive = true,deliveryFee=45},
                    new Area { name = "الشيخ زايد" , isActive = true,deliveryFee=50}
                },
                isActive=true
            },
            new Governorate
            {
               name = "الإسكندرية",
               areas = new List<Area>
               {
                   new Area { name = "سيدي جابر" ,isActive=true,deliveryFee=55},
                   new Area { name = "محرم بك" ,isActive=true ,deliveryFee=60},
                   new Area { name = "العجمي" ,isActive=true ,deliveryFee=70}
                },
                isActive=true
             },
            new Governorate
            {
                name = "المنصورة",
                areas = new List<Area>
                {
                    new Area { name = "المنصورة الجديدة",isActive=true ,deliveryFee=75},
                    new Area { name = "ميت غمر" ,isActive=true ,deliveryFee=80}
                },
                isActive=true
            }
           };
            await _context.Governorate.AddRangeAsync(governorates);
            await _context.SaveChangesAsync();
        }
    }

    }
