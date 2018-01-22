using MapsProject.Models;
using System.Web.Mvc;

namespace MapsProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [HttpGet]
        public ActionResult Moderate()
        {
            return View();
        }

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
