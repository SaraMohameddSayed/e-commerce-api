
using Infrastructure;
using Managers.Events;
using Managers.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
namespace Managers;

 public class offerManager : MainManager<Offer>
    {
    public IEventBus eventBus;
    public UserManager<IdentityUser> UserManager;
    public offerManager(dbContext _context,IEventBus _eventBus,UserManager<IdentityUser> _userManager) : base(_context)
        {
        eventBus = _eventBus;
        UserManager = _userManager;
    }
   public async Task<bool> AddOfferAsync(Offer _offer)
    {
        var result = await Add(_offer);
        if (result)
        {
            var offer = await dbContext.Set<Offer>().FirstOrDefaultAsync(o => o.name == _offer.name);

            var adminUsers = await UserManager.GetUsersInRoleAsync("Admin");
            var adminIds = adminUsers.Select(u => u.Id).ToHashSet();
            var userIds = await dbContext.Set<IdentityUser>()
          .Where(u => !adminIds.Contains(u.Id))
          .Select(u => u.Id)
          .ToListAsync();

            await eventBus.Publish(new NewOfferAddedEvent(userIds, offer.id));
        }
        return result;
    }
}
