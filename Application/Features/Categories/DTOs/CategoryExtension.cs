using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
namespace DTOs;

    public static class CategoryExtensions
    {
        public static Category ToCategory(this AddCategoryRequest AddCategoryRequest)
        {
           return new Category
           {
               Name=AddCategoryRequest.Name
           };
        }
    public static CategoryResponse ToResponse(this Category model)
        {
            return new CategoryResponse
            {
                Id=model.Id,
                Name=model.Name
            };
    }
}

