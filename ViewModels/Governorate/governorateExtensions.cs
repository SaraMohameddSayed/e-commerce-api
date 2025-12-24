using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
namespace ViewModels
{
    public static class governorateExtensions
    {
        public static governorateViewModel ToViewModel(this Governorate governorate)
        {
            if (governorate == null) return null;
            return new governorateViewModel
            {
                id = governorate.id,
                name = governorate.name,
                deliveryFee = governorate.deliveryFee,
                areas = governorate.areas?.Select(a => a.ToViewModel()).ToList()
            };
        }

        public static Governorate toModel(this governorateViewModel governorateViewModel)
        {
            if (governorateViewModel == null) return null;
            return new Governorate
            {
                id = governorateViewModel.id,
                name = governorateViewModel.name,
                deliveryFee = governorateViewModel.deliveryFee,
                areas = governorateViewModel.areas?.Select(a => a.toModel()).ToList()
            };
        }
    }
}
