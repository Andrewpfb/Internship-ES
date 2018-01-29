using System.Web.Mvc;

namespace MapsProject.WEB.Areas.Administration
{
    public class AdministrationAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Administration";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Administration_default",
                "Administration/{controller}/{action}/{id}",
                new {controller = "Auth", action = "Moderate", id = UrlParameter.Optional }
            );
        }
    }
}