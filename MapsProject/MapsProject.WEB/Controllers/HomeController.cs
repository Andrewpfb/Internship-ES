using MapsProject.WEB.Models;
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

        /// <summary>
        /// Method for calling authorization page. GET-version.
        /// </summary>
        /// <returns>View() for authorization page.</returns>
        [HttpGet]
        public ActionResult Moderate()
        {
            return View();
        }

        /// <summary>
        /// Method for calling authorization page. POST-version.
        /// </summary>
        /// <param name="user">Authorized user.</param>
        /// <returns>If authorization is passed, then returns a page with unapproved objects.
        /// If it is not passed, then the GET-version of the method is called 
        /// and an error message is sent to it.</returns>
        [HttpPost, ValidateAntiForgeryToken, ActionName("Moderate")]
        public ActionResult ConfirmModerate([Bind(Include = "Username, Password")]User user)
        {
            if (ModelState.IsValid)
            {
                if (user.Username == "Admin" & user.Password == "Password")
                {
                    return View("AdminIsLogged");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login or password");
                }
            }
            else
            {
                ModelState.AddModelError("", "Incorrect login or password");
            }
            return View(user);
        }
    }
}
