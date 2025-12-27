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
        public governorateManager governorateManager;
        public areaManager(dbContext _context,governorateManager _governorateManager) : base(_context)
        {
            context = _context;
            governorateManager = _governorateManager;
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
            var governorate =await governorateManager.getOne(area.governorateId);
            if (area.isActive == false && !governorate.areas.Any(a => a.isActive == true))
            {
                governorate.isActive = false;
                await governorateManager.Update(governorate);
            }
            if (area.isActive == true)
            {
                governorate.isActive=true;
                await governorateManager.Update(governorate);

            }
            await context.SaveChangesAsync();
            return true;
        }
    }
}
