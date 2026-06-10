using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using DTOs;
namespace DTOs
{
    public static class GovernorateExtensions
    {
        public static GovernorateResponse ToResponse(this Governorate governorate)
        {
            if (governorate == null) return null;
            return new GovernorateResponse
            {
                Id = governorate.Id,
                Name = governorate.Name,
                IsActive = governorate.IsActive,
                Areas = governorate.Areas?.Select(a => a.ToResponse()).ToList()
            };
        }

        public static Governorate ToGovernorate(this AddGovernorateRequest addGovernorateRequest)
        {
            if (addGovernorateRequest == null) return null;
            return new Governorate
            {
                Name = addGovernorateRequest.Name,
            };
        }
    }
}
