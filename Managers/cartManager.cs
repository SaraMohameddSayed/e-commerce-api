using System;
using Models;
using Infrastructure;
namespace Managers;

 public class cartManager : MainManager<Cart>
    {
        public cartManager(dbContext _context) : base(_context)
        {

        }
    }
