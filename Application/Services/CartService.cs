using System;
using Domain;
using Infrastructure;
namespace Application.Services;

public class CartService : MainService<Cart>
    {
        public CartService(AppDbContext AppDbContext) : base(AppDbContext)
        {

        }
    }
