using Application.Features.Delivery.Governorate.DTOs;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;
namespace Services
{
    public class GovernorateService:MainService<Governorate>
    {
        public dbContext _context;
        public GovernorateService(dbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> UpdateGovernorate(updateGovernorateViewModel updateGovernorateViewModel)
        {
            var governorate = await GetOne(updateGovernorateViewModel.id);
            if (governorate == null)
            {
                return false;
            }
            governorate.name = updateGovernorateViewModel.name;
            _context.Update(governorate);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateGovernorateStatus(int id)
        {
            var governorate = await GetOne(id);
            if (governorate == null)
            {
                return false;
            }
            governorate.isActive = !governorate.isActive;
            var areas = await _context.Set<Area>().Where(a => a.governorateId == id).ToListAsync();
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
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
