using System;
using Models;
using Infrastructure;
namespace Managers;

 public class productManager : MainManager<Product>
    {
        public productManager(dbContext _context) : base(_context)
        {

        }
    }
