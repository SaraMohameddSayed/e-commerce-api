using Infrastructure;
using Domain;
using DTOs;
namespace Services
{
    public class AreaService:MainService<Area>
    {
        public dbContext _context;
        public GovernorateService _governorateManager;
        public AreaService(dbContext context,GovernorateService governorateManager) : base(context)
        {
            _context = context;
            _governorateManager = governorateManager;
        }
        public async Task<bool> UpdateArea(UpdateAreaRequest updateAreaRequest)
        {
            var area = await GetOne(updateAreaRequest.id);
            if (area == null)
            {
                return false;
            }
            area.name = updateAreaRequest.name;
            area.deliveryFee = updateAreaRequest.deliveryFee;

            _context.Update(area);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAreaStatus(int id)
        {
            var area = await GetOne(id);
            if (area == null)
            {
                return false;
            }
            area.isActive = !area.isActive;
            var governorate =await _governorateManager.GetOne(area.governorateId);
            if (area.isActive == false && !governorate.areas.Any(a => a.isActive == true))
            {
                governorate.isActive = false;
                await _governorateManager.Update(governorate);
            }
            if (area.isActive == true)
            {
                governorate.isActive=true;
                await _governorateManager.Update(governorate);

            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
