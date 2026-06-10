
using Infrastructure;
using Application.Events;
using Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Domain;
using System;
namespace Application.Services;;

 public class OfferService : MainService<Offer>
    {
    private readonly IEventBus _eventBus;
    private readonly UserManager<IdentityUser> _userManager;
    public OfferService(AppDbContext AppDbContext, IEventBus eventBus,UserManager<IdentityUser> userManager) : base(AppDbContext)
        {
        _eventBus = eventBus;
        _userManager = userManager;
    }
   public async Task<bool> AddOfferAsync(Offer _offer)
    {
        var result = await Add(_offer);
        if (result)
        {
            var offer = await _AppDbContext.Set<Offer>().FirstOrDefaultAsync(o => o.Name == _offer.Name);

            var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
            var adminIds = adminUsers.Select(u => u.Id).ToHashSet();
            var userIds = await _AppDbContext.Set<IdentityUser>()
          .Where(u => !adminIds.Contains(u.Id))
          .Select(u => u.Id)
          .ToListAsync();

            await _eventBus.Publish(new NewOfferAddedEvent(userIds, offer.Id));
        }
        return result;
    }
}
