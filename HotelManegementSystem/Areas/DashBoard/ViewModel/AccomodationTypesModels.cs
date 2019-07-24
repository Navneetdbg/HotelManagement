using HmsEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManegementSystem.Areas.DashBoard.ViewModel
{
    public class AccomodationTypesListingModels
    {
      
        public List<AccomodationType> AccomodationTypes { get; set; }
        public string Search { get;  set; }
    }

    public class AccomodationTypesActionModels
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
    }
}