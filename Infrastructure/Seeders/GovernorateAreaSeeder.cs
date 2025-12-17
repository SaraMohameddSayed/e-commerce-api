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
                    new Area { name = "مدينة نصر" },
                    new Area { name = "المعادي" },
                    new Area { name = "الزمالك" },
                    new Area { name = "حلوان" }
                }
            },
            new Governorate
            {
                name = "الجيزة",
                deliveryFee = 25,
                areas = new List<Area>
                {
                    new Area { name = "الدقي" },
                    new Area { name = "المهندسين" },
                    new Area { name = "6 أكتوبر" },
                    new Area { name = "الشيخ زايد" }
                }
            },
            new Governorate
            {
               name = "الإسكندرية",
               deliveryFee = 30,
               areas = new List<Area>
               {
                   new Area { name = "سيدي جابر" },
                   new Area { name = "محرم بك" },
                   new Area { name = "العجمي" }
                }
             },
            new Governorate
            {
                name = "المنصورة",
                deliveryFee = 15,
                areas = new List<Area>
                {
                    new Area { name = "المنصورة الجديدة" },
                    new Area { name = "ميت غمر" }
                }
            }
           };
            await _context.Governorate.AddRangeAsync(governorates);
            await _context.SaveChangesAsync();
        }
    }

    }
