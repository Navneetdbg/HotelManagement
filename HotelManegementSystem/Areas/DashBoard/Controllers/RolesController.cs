using HmsEntity;
using HmsServices;
using HotelManegementSystem.Areas.DashBoard.ViewModel;
using HotelManegementSystem.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
    public class RolesController : Controller
    {
        // GET: DashBoard/Roles
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
        public RolesController()
        {
        }

        public RolesController(HMSUserManager userManager, UsersSignInManager signInManager , HmsRoleManeger roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }




        // GET: DashBoard/Users
        public ActionResult Index(string search, int? pageNo)
        {

            RoleListingModels models = new RoleListingModels();
            int pageSize = 10;
            pageNo = pageNo ?? 1;
            models.search = search;
            models.Roles = SearchAllRoles(search, pageNo.Value, pageSize);
           
            var totalRecords = SearchRolesCount(search);
            models.Pager = new Pager(totalRecords, pageNo, pageSize);
            return View(models);
        }

        public List<IdentityRole> SearchAllRoles(string search, int pageNo, int pageSize)
        {

            var roles = RoleManager.Roles.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                roles = roles.Where(a => a.Name.ToLower().Contains(search.ToLower()));
            }

            return roles.OrderBy(x => x.Name).Skip((pageNo - 1) * pageSize).
                        Take(pageSize).ToList();

           

        }

        public int SearchRolesCount(string search)
        {

            var roles = RoleManager.Roles.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                roles = roles.Where(a => a.Name.ToLower().Contains(search.ToLower()));
            }

            return roles.Count();
           

        }

        public async Task<ActionResult> Action(string Id)
        {
            RolesViewModels models = new RolesViewModels();
            if (!string.IsNullOrEmpty(Id))
            {
                var roles = await RoleManager.FindByIdAsync(Id);
                models.Id = roles.Id;
                models.Name = roles.Name;

            }
            // models.AccomodationPackages = AccomodationPackagesServices.Instance.GetAllAccomodationPackages();
            return PartialView("_Action", models);
        }

        [HttpPost]
        public async Task<JsonResult> Action(RolesViewModels model)
        {
            JsonResult json = new JsonResult();
            IdentityResult result = null;
            if (!string.IsNullOrEmpty(model.Id))
            {
                var roles = await RoleManager.FindByIdAsync(model.Id);
                roles.Name = model.Name;
                
                result = await RoleManager.UpdateAsync(roles);

            }
            else
            {
                var roles = new IdentityRole();
                roles.Name = model.Name;
                
                result = await RoleManager.CreateAsync(roles);
            }

            json.Data = new { Success = result.Succeeded, Message = string.Join(", ", result.Errors) };
            return json;


        }
        public async Task<ActionResult> Delete(string Id)
        {
            RolesViewModels models = new RolesViewModels();
            var users = await RoleManager.FindByIdAsync(Id);

            models.Id = users.Id;
            return PartialView("_Delete", models);

        }

        [HttpPost]
        public async Task<JsonResult> Delete(RolesViewModels models)
        {
            JsonResult json = new JsonResult();
            IdentityResult result = null;
            if (!string.IsNullOrEmpty(models.Id))
            {
                var users = await RoleManager.FindByIdAsync(models.Id);

                result = await RoleManager.DeleteAsync(users);
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