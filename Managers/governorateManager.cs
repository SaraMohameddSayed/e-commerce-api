using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
namespace Managers
{
    public class governorateManager:MainManager<Governorate>
    {
        public dbContext context;
        public governorateManager(dbContext _context) : base(_context)
        {
            context = _context;
        }

        public async Task<bool> updateGovernorate(updateGovernorateViewModel updateGovernorateViewModel)
        {
            var governorate = await getOne(updateGovernorateViewModel.id);
            if (governorate == null)
            {
                return false;
            }
            governorate.name = updateGovernorateViewModel.name;
            governorate.deliveryFee = updateGovernorateViewModel.deliveryFee;
            context.Update(governorate);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> softDeleteGovernorate(int id)
        {
            var governorate = await getOne(id);
            if (governorate == null)
            {
                return false;
            }
            governorate.isActive = false;
            var areas = await context.Set<Area>().Where(a => a.governorateId == id).ToListAsync();
            foreach (var area in areas)
            {
                area.isActive = false;
            }
            await context.SaveChangesAsync();
            return true;
        }
    }
}
