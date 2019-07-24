using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HotelManegementSystem.Startup))]
namespace HotelManegementSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
