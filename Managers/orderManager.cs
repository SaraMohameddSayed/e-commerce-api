using System;
using Models;
using Infrastructure;
namespace Managers;

 public class orderManager : MainManager<Order>
    {
        public orderManager(dbContext _context) : base(_context)
        {

        }
    }
