using System.Web.Mvc;

namespace MapsProject.WEB.Controllers
{
    /// <summary>
    /// Controller for page call. 
    /// Has methods for calling the main page and authorization page. 
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Method for calling main page. GET-version.
        /// </summary>
        /// <returns>View() for main page.</returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
