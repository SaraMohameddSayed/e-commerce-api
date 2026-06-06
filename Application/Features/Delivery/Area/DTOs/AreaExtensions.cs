using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
namespace DTOs
{
    public static class areaExtensions
    {
        public static AreaResponse ToViewModel(this Area area)
        {
            if (area == null) return null;
            return new AreaResponse
            {
                id = area.id,
                name = area.name,
                isActive= area.isActive,
                deliveryFee = area.deliveryFee,
                governorateId = area.governorateId
            };
        }
        public static Area toModel(this AddAreaRequest addAreaRequest)
        {
            if (addAreaRequest == null) return null;
            return new Area
            {
                name = addAreaRequest.name,
                deliveryFee = addAreaRequest.deliveryFee,
                governorateId = addAreaRequest.governorateId
            };
        }
    }
}
