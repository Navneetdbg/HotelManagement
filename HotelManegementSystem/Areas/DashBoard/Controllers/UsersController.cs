using HmsEntity;
using HmsServices;
using HotelManegementSystem.Areas.DashBoard.ViewModel;
using HotelManegementSystem.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HotelManegementSystem.Areas.DashBoard.Controllers
{
    [Authorize(Roles = "Manager")]
    public class UsersController : Controller
    {
        private UsersSignInManager _signInManager;
        private HMSUserManager _userManager;
        private HmsRoleManeger _roleManager;
        public UsersSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<UsersSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public HMSUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<HMSUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public HmsRoleManeger RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().GetUserManager<HmsRoleManeger>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        public UsersController()
        {
        }

        public UsersController(HMSUserManager userManager, UsersSignInManager signInManager , HmsRoleManeger roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }
        
        // GET: DashBoard/Users
        public async Task<ActionResult> Index(string search, string roleId, int? pageNo)
        {
            
        UsersListingModels models = new UsersListingModels();
            int pageSize = 10;
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            models.Search = search;
            models.RoleId = roleId;
            models.Roles = RoleManager.Roles.ToList();
            models.Users = await SearchAllUsers(search, roleId , pageNo.Value, pageSize);
            //  models.Roles = AccomodationPackagesServices.Instance.GetAllAccomodationPackages();
            var totalRecords = await SearchUsersCount(search, roleId); //AccomodationPackagesServices.Instance.SearchAllAccomodationPackagesCount(search, roleId);
            models.pager =  new Pager(totalRecords, pageNo, pageSize);
            return View(models);
        }

        public async Task< List<HmsUser>> SearchAllUsers(string search, string roleId, int pageNo, int pageSize)
        {

            var users = UserManager.Users.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                users = users.Where(a => a.Email.ToLower().Contains(search.ToLower()));
            }
            if (!string.IsNullOrEmpty(roleId))
            {
                
                var role = await RoleManager.FindByIdAsync(roleId);
                var userIds = role.Users.Select(x => x.UserId).ToList();
                users = users.Where(x => userIds.Contains(x.Id));
            }
            return users.OrderBy(x => x.Email).Skip((pageNo - 1) * pageSize).
                        Take(pageSize).ToList();

        }

        public async Task <int> SearchUsersCount(string search, string roleId)
        {

            var users = UserManager.Users.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                users = users.Where(a => a.Email.ToLower().Contains(search.ToLower()));
            }
            if (!string.IsNullOrEmpty(roleId))
            {
                var role = await RoleManager.FindByIdAsync(roleId);
                var userIds = role.Users.Select(x => x.UserId).ToList();
                users = users.Where(x => userIds.Contains(x.Id));
            }
            return users.Count();

        }
       
        public async Task<ActionResult> Action(string Id)
        {
            UsersModels models = new UsersModels();
            if (!string.IsNullOrEmpty(Id))
            {
                var users = await UserManager.FindByIdAsync(Id);
                models.Id = users.Id;
                models.UserName = users.UserName;
                models.FullName = users.FullName;
                models.City = users.City;
                models.Country = users.Country;
                models.Address = users.Address;
                models.Email = users.Email;

            }
           // models.AccomodationPackages = AccomodationPackagesServices.Instance.GetAllAccomodationPackages();
            return PartialView("_Action", models);
        }

        [HttpPost]
        public async Task<JsonResult> Action(UsersModels model)
        {
            JsonResult json = new JsonResult();
            IdentityResult result = null;
            if (!string.IsNullOrEmpty(model.Id))
            {
                var users = await UserManager.FindByIdAsync(model.Id);
                //var roles = await RoleManager.FindByNameAsync("User");
                users.FullName = model.FullName;
                users.Email = model.Email;
                users.Address = model.Address;
                users.UserName = model.UserName;
                users.City = model.City;
                users.Country = model.Country;
                
                //users.Roles = roles.Name;
                result = await UserManager.UpdateAsync(users);
               
            }
            else
            {
                var users = new HmsUser();
                users.FullName = model.FullName;
                users.Email = model.Email;
                users.Address = model.Address;
                users.UserName = model.UserName;
                users.City = model.City;
                users.Country = model.Country;
                //users.PasswordHash = "100%cool";
                result = await UserManager.CreateAsync(users);
            }

            json.Data = new { Success = result.Succeeded, Message = string.Join(", ", result.Errors) };
            return json;


        }
        public async  Task<ActionResult> Delete(string Id)
        {
            UsersModels models = new UsersModels();
            var users = await UserManager.FindByIdAsync(Id);
            models.Id = users.Id;
            return PartialView("_Delete", models);

        }

        [HttpPost]
        public async Task<JsonResult> Delete(UsersModels models)
        {
            JsonResult json = new JsonResult();
            IdentityResult result = null;
            if (!string.IsNullOrEmpty(models.Id))
            {
                var users = await UserManager.FindByIdAsync(models.Id);

                result = await UserManager.DeleteAsync(users);
                json.Data = new { Success = result.Succeeded, Message = string.Join(", ", result.Errors) };
            }
            else
            {
                json.Data = new { Success = false, Message = "No User Found" };
            }
            return json;
        }

        public async Task <ActionResult> UserRoles(string Id)
        {
            var users = await UserManager.FindByIdAsync(Id);
            UsersRolesModels models = new UsersRolesModels();
            models.UserId = Id;
            var userRoleId = users.Roles.Select(x => x.RoleId).ToList();
            
            models.UserRoles = RoleManager.Roles.Where(x => userRoleId.Contains(x.Id)).ToList();
            models.Roles = RoleManager.Roles.Where(z=> !userRoleId.Contains(z.Id)).ToList();
            return PartialView("_UserRoles", models);
        }


        [HttpPost]
        public async Task<JsonResult> UserRolesAction(string UserId , string roleId , bool isDelete = false)
        {
            JsonResult json = new JsonResult();
            var User = await UserManager.FindByIdAsync(UserId);
            var Role = await RoleManager.FindByIdAsync(roleId);
           
            if(User != null && Role != null)
            {
                IdentityResult result = null;
                if (!isDelete)
                {
                     result = await UserManager.AddToRoleAsync(UserId, Role.Name);
                }
                else
                {
                     result = await UserManager.RemoveFromRoleAsync(UserId, Role.Name);
                }
                json.Data = new { Success = result.Succeeded, Message = string.Join(", ", result.Errors) };
            }
            else
            {
                json.Data = new { Success = false, Message = "No User Found" };
            }
            return json;
        }
    }
}