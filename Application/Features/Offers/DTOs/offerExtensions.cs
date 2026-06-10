using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public static class OfferExtensions
    {
       public static OfferResponse ToResponse(this Offer offer)
        {
            return new OfferResponse
            {
                Id = offer.Id,
                Name = offer.Name,
                Discount = offer.Discount,
                StartDate = offer.StartDate ?? DateTime.Now,
                EndDate = offer.EndDate ?? DateTime.Now
            };
        }

    }
}
