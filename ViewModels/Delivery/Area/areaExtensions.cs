using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
namespace ViewModels
{
    public static class areaExtensions
    {
        public static areaViewModel ToViewModel(this Area area)
        {
            if (area == null) return null;
            return new areaViewModel
            {
                id = area.id,
                name = area.name,
                isActive= area.isActive,
                deliveryFee = area.deliveryFee,
                governorateId = area.governorateId
            };
        }
        public static Area toModel(this addAreaViewModel addAreaViewModel)
        {
            if (addAreaViewModel == null) return null;
            return new Area
            {
                name = addAreaViewModel.name,
                deliveryFee = addAreaViewModel.deliveryFee,
                governorateId = addAreaViewModel.governorateId
            };
        }
    }
}
