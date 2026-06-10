using Infrastructure;
using Domain;
using DTOs;
namespace Application.Services 
{
    public class AreaService:MainService<Area>
    {
        private readonly AppDbContext _AppDbContext;
        private readonly GovernorateService _governorateService;
        public AreaService(AppDbContext AppDbContext, GovernorateService governorateService) : base(AppDbContext)
        {
            _AppDbContext = AppDbContext;
            _governorateService = governorateService;
        }
        public async Task<bool> UpdateArea(UpdateAreaRequest updateAreaRequest)
        {
            var area = await GetOne(updateAreaRequest.Id);
            if (area == null)
            {
                return false;
            }
            area.Name = updateAreaRequest.Name;
            area.DeliveryFee = updateAreaRequest.DeliveryFee;

            _AppDbContext.Update(area);
            await _AppDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAreaStatus(int id)
        {
            var area = await GetOne(id);
            if (area == null)
            {
                return false;
            }
            area.IsActive = !area.IsActive;
            var governorate =await _governorateService.GetOne(area.GovernorateId);
            if (area.IsActive == false && !governorate.Areas.Any(a => a.IsActive == true))
            {
                governorate.IsActive = false;
                await _governorateService.Update(governorate);
            }
            if (area.IsActive == true)
            {
                governorate.IsActive = true;
                await _governorateService.Update(governorate);

            }
            await _AppDbContext.SaveChangesAsync();
            return true;
        }
    }
}
