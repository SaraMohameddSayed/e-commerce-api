
using System;
using Domain;
using Infrastructure;

namespace Services;
 public class CategoryService : MainService<Category>
    {
        public CategoryService(dbContext _context) : base(_context)
        {

        }
    }
