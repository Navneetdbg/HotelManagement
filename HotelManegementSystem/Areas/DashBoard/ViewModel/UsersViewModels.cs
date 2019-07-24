using HmsEntity;
using HotelManegementSystem.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManegementSystem.Areas.DashBoard.ViewModel
{
    public class UsersModels
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }
        //public string RoleId { get; set; }
        //public virtual IdentityRole Role { get; set; }
        
        //public List<IdentityRole> Roles { get; set; }
    }

    public class UsersListingModels
    {
        public List<HmsUser> Users { get; set; }
        public string Search { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public string RoleId { get; set; }
        public Pager pager { get; set; }
    }

    public class UsersRolesModels
    {
        public string UserId { get; set; }
        public List<IdentityRole> UserRoles { get; set; }
        public List<IdentityRole> Roles { get; set; }
    }
}