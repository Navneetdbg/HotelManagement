using HmsEntity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HmsServices
{
   public class UsersSignInManager : SignInManager<HmsUser, string>
    {
        public UsersSignInManager(HMSUserManager userManager, IAuthenticationManager authenticationManager)
          : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(HmsUser user)
        {
            return user.GenerateUserIdentityAsync((HMSUserManager)UserManager);
        }

        public static UsersSignInManager Create(IdentityFactoryOptions<UsersSignInManager> options, IOwinContext context)
        {
            return new UsersSignInManager(context.GetUserManager<HMSUserManager>(), context.Authentication);
        }

    }
}
