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
        public static Category toCategoryModel(this AddCategoryRequest model)
        {
           return new Category
           {
               name=model.name
           };
        }
    public static CategoryResponse toViewModel(this Category model)
        {
            return new CategoryResponse
            {
                id=model.id,
                name=model.name
            };
    }
}

