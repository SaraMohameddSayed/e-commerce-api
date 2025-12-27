using Infrastructure;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
namespace Managers
{
    public class areaManager:MainManager<Area>
    {
        public dbContext context;

        public areaManager(dbContext _context) : base(_context)
        {
            context = _context;
        }
        public async Task<bool> updateArea(updateAreaViewModel updateAreaViewModel)
        {
            var area = await getOne(updateAreaViewModel.id);
            if (area == null)
            {
                return false;
            }
            area.name = updateAreaViewModel.name;
            area.deliveryFee = updateAreaViewModel.deliveryFee;

            context.Update(area);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> updateAreaStatus(int id)
        {
            var area = await getOne(id);
            if (area == null)
            {
                return false;
            }
            area.isActive = !area.isActive;
            await context.SaveChangesAsync();
            return true;
        }
    }
}
