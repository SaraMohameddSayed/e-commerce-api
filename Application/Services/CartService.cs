using System;
using Domain;
using Infrastructure;
namespace Services;

 public class CartService : MainService<Cart>
    {
        public CartService(dbContext _context) : base(_context)
        {

        }
    }
