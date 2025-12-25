using Models;
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
                deliveryFee = 20,
                areas = new List<Area>
                {
                    new Area { name = "مدينة نصر" ,isActive=true},
                    new Area { name = "المعادي" ,isActive=true},
                    new Area { name = "الزمالك" ,isActive=true},
                    new Area { name = "حلوان" , isActive=true}
                },
                isActive=true
            },
            new Governorate
            {
                name = "الجيزة",
                deliveryFee = 25,
                areas = new List<Area>
                {
                    new Area { name = "الدقي",isActive=true },
                    new Area { name = "المهندسين" , isActive = true},
                    new Area { name = "6 أكتوبر" , isActive = true},
                    new Area { name = "الشيخ زايد" , isActive = true}
                },
                isActive=true
            },
            new Governorate
            {
               name = "الإسكندرية",
               deliveryFee = 30,
               areas = new List<Area>
               {
                   new Area { name = "سيدي جابر" ,isActive=true},
                   new Area { name = "محرم بك" ,isActive=true },
                   new Area { name = "العجمي" ,isActive=true }
                },
                isActive=true
             },
            new Governorate
            {
                name = "المنصورة",
                deliveryFee = 15,
                areas = new List<Area>
                {
                    new Area { name = "المنصورة الجديدة",isActive=true },
                    new Area { name = "ميت غمر" ,isActive=true }
                },
                isActive=true
            }
           };
            await _context.Governorate.AddRangeAsync(governorates);
            await _context.SaveChangesAsync();
        }
    }

    }
