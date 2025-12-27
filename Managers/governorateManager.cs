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
            context.Update(governorate);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> updateGovernorateStatus(int id)
        {
            var governorate = await getOne(id);
            if (governorate == null)
            {
                return false;
            }
            governorate.isActive = !governorate.isActive;
            var areas = await context.Set<Area>().Where(a => a.governorateId == id).ToListAsync();
            foreach (var area in areas)
            {
                if (governorate.isActive == false) {
                    area.isActive = false;

                }
                else
                {
                    area.isActive = true;

                }
            }
            await context.SaveChangesAsync();
            return true;
        }
    }
}
