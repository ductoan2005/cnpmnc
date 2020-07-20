using System.Web.Mvc;

namespace DoAn.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.MapMvcAttributeRoutes();
            //context.MapRoute(
            //    "Custom",
            //    "{action}/{id}",
            //    new { controller = "Home" ,action = "Index", id = UrlParameter.Optional },
            //    new {controller = "Home|ThuongHieu" }
            //);
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "DoAn.Areas.Admin.Controllers" }
            );
        }
    }
}