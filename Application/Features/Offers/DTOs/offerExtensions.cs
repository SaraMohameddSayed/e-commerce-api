using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public static class offerExtensions
    {
       public static offerViewModel toViewModel(this Offer offer)
        {
            return new offerViewModel
            {
                id = offer.id,
                name = offer.name,
                discount = offer.discount,
                startDate = offer.startDate ?? DateTime.Now,
                endDate = offer.endDate ?? DateTime.Now
            };
        }

    }
}
