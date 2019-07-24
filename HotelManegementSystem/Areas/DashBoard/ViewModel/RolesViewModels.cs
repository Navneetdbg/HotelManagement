using HotelManegementSystem.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManegementSystem.Areas.DashBoard.ViewModel
{
    public class RolesViewModels
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }

    public class RoleListingModels
    {
        public List<IdentityRole> Roles { get; set; }
        public string search { get; set; }

        public Pager Pager { get; set; }
    }
}