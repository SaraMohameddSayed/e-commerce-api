
using System;
using Models;
using Infrastructure;

namespace Managers;
 public class categoryManager : MainManager<Category>
    {
        public categoryManager(dbContext _context) : base(_context)
        {

        }
    }
