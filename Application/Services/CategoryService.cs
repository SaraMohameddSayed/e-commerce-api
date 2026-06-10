
using System;
using Domain;
using Infrastructure;

namespace Application.Services;
public class CategoryService : MainService<Category>
    {
        public CategoryService(AppDbContext AppDbContext) : base(AppDbContext)
        {

        }
    }
