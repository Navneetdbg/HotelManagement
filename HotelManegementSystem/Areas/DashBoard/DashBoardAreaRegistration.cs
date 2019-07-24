using System.Web.Mvc;

namespace HotelManegementSystem.Areas.DashBoard
{
    public class DashBoardAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DashBoard";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DashBoard_default",
                "DashBoard/{controller}/{action}/{id}",
                new {controller="DashBoard" ,action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}