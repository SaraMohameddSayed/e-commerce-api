using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Delivery.Governorate.DTOs;
using Domain;
using DTOs;
namespace DTOs
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
                isActive=governorate.isActive,
                areas = governorate.areas?.Select(a => a.ToViewModel()).ToList()
            };
        }

        public static Governorate toModel(this addGovernorateViewModel addGovernorateViewModel)
        {
            if (addGovernorateViewModel == null) return null;
            return new Governorate
            {
                name = addGovernorateViewModel.name,
            };
        }
    }
}
