using HmsEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HmsEntity
{
    public class JoinViewModel
    {
        public Booking Booking { get; set; }

        public BookingUsers BookingUsers { get; set; }
    }
}