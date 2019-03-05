using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace thebourne.Helpers
{
    public static class ReservationHelper
    {

        public static string GetReservationClass(bool reserved)
        {
            if (reserved)
                return "panel-danger";
            return "panel-info";
        }
    }
}