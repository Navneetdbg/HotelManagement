using HmsEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManegementSystem.ViewModels
{
    public class BookingViewModels
    {
        public List<Accomodation> Accomodations { get; set; }

        public int TotalRooms { get; set; }
        public int AccomodationPackageId { get; set; }
        public AccomodationPackage AccomodationPackage { get; set; }
        public int Id { get; set; }
        
        public DateTime FromDate { get; set; }
        
        public DateTime Duration { get; set; }
        
        public string UserId { get; set; }

        public virtual HmsUser Users { get; set; }

       
    }
}