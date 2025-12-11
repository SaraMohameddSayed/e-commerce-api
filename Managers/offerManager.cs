
using System;
using Models;
using Infrastructure;
namespace Managers;

 public class offerManager : MainManager<Offer>
    {

    public offerManager(dbContext _context) : base(_context)
        {

        }

     }
