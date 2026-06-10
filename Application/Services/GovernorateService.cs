using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Domain;
using DTOs;

namespace Application.Services
{
    public class GovernorateService:MainService<Governorate>
    {
        private readonly AppDbContext _AppDbContext;
        public GovernorateService(AppDbContext AppDbContext) : base(AppDbContext)
        {
            _AppDbContext = AppDbContext;
        }

        public async Task<bool> UpdateGovernorate(UpdateGovernorateRequest updateGovernorateRequest)
        {
            var governorate = await GetOne(updateGovernorateRequest.Id);
            if (governorate == null)
            {
                return false;
            }
            governorate.Name = updateGovernorateRequest.Name;
            _AppDbContext.Update(governorate);
            await _AppDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateGovernorateStatus(int id)
        {
            var governorate = await GetOne(id);
            if (governorate == null)
            {
                return false;
            }
            governorate.IsActive = !governorate.IsActive;
            var areas = await _AppDbContext.Set<Area>().Where(a => a.GovernorateId == id).ToListAsync();
            foreach (var area in areas)
            {
                if (governorate.IsActive == false) {
                    area.IsActive = false;
                }
                else
                {
                    area.IsActive = true;
                }
            }
            await _AppDbContext.SaveChangesAsync();
            return true;
        }
    }
}
