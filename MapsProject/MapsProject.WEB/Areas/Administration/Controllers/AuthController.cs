using System.Web.Mvc;

namespace MapsProject.WEB.Areas.Administration.Controllers
{
    public class AuthController : Controller
    {

        // GET: Administration/Auth
        /// <summary>
        /// Method for calling authorization page. GET-version.
        /// </summary>
        /// <returns>View() for authorization page.</returns>
        [System.Web.Mvc.HttpGet]
        public ActionResult Login()
        {
            return View();
        }
    }
}