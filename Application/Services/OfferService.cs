
using Infrastructure;
using Services.Events;
using Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Domain;
using System;
namespace Services;

 public class OfferService : MainService<Offer>
    {
    public IEventBus _eventBus;
    public UserManager<IdentityUser> _userManager;
    public OfferService(dbContext context,IEventBus eventBus,UserManager<IdentityUser> userManager) : base(context)
        {
        _eventBus = eventBus;
        _userManager = userManager;
    }
   public async Task<bool> AddOfferAsync(Offer _offer)
    {
        var result = await Add(_offer);
        if (result)
        {
            var offer = await dbContext.Set<Offer>().FirstOrDefaultAsync(o => o.name == _offer.name);

            var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
            var adminIds = adminUsers.Select(u => u.Id).ToHashSet();
            var userIds = await dbContext.Set<IdentityUser>()
          .Where(u => !adminIds.Contains(u.Id))
          .Select(u => u.Id)
          .ToListAsync();

            await _eventBus.Publish(new NewOfferAddedEvent(userIds, offer.id));
        }
        return result;
    }
}
