using HmsDatabase;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmsServices
{
   public class HmsRoleManeger : RoleManager<IdentityRole>
    {
        public HmsRoleManeger(IRoleStore<IdentityRole , string>  roleStore): base(roleStore)
        {

        }

        public static HmsRoleManeger create(IdentityFactoryOptions<HmsRoleManeger> options , IOwinContext context)
        {
            return new HmsRoleManeger(new RoleStore<IdentityRole>(context.Get<HmsContext>()));
        }
    }
}
