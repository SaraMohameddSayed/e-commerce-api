using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
namespace ViewModels;

    public static class categoryExtensions
    {
        public static Category toCategoryModel(this addCategoryViewModel model)
        {
           return new Category
           {
               name=model.name
           };
        }
    public static categoryViewModel toViewModel(this Category model)
        {
            return new categoryViewModel
            {
                id=model.id,
                name=model.name
            };
    }
}

