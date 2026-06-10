using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
namespace DTOs
{
    public static class AreaExtensions
    {
        public static AreaResponse ToResponse(this Area area)
        {
            if (area == null) return null;
            return new AreaResponse
            {
                Id = area.Id,
                Name = area.Name,
                IsActive= area.IsActive,
                DeliveryFee = area.DeliveryFee,
                GovernorateId = area.GovernorateId  
            };
        }
        public static Area ToArea(this AddAreaRequest addAreaRequest)
        {
            if (addAreaRequest == null) return null;
            return new Area
            {
                Name = addAreaRequest.Name,
                DeliveryFee = addAreaRequest.DeliveryFee,
                GovernorateId = addAreaRequest.GovernorateId
            };
        }
    }
}
