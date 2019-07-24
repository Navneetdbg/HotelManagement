﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmsEntity
{
   public class Booking
    {
        public int Id { get; set; }
        
        public DateTime FromDate { get; set; }
        public DateTime EndDate { get; set; }

        public string UserId { get; set; }

        public virtual HmsUser Users { get; set; }

        public virtual List<BookingUsers> BookingUsers { get; set; }

        
    }
}
