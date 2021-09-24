using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dii_OrderingSvc.Assets
{
    public class OrderingLogMessageTemplates: onbe.logging.templates.IOnbeLoggingMessageTemplates
    {
        public const string LOG_booking_Message = "Booking for theatercode {theaterCode} - {@Booking}";
        public const string RETRIEVED_movie_FROM_MOVIECATALOGSVC = "Retrieved Movies from Movie Catalog - {@Booking}";
    }
}
