using MapsProject.WEB.Models;
using System.Web.Mvc;

namespace MapsProject.WEB.Controllers
{
    /// <summary>
    /// Контроллер для вызова страниц. Имеет методы для вызоваглавной страницы 
    /// и страницы авторизации для модерации. 
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Метод для вызова главной страницы. GET-версия.
        /// </summary>
        /// <returns>Возвращает View() для главной страницы.</returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        /// <summary>
        /// Метод для вызова страницы авторизации. GET-версия.
        /// </summary>
        /// <returns>Возвращает View() для страницы авторизации.</returns>
        [HttpGet]
        public ActionResult Moderate()
        {
            return View();
        }

        /// <summary>
        /// Метод для вызова страницы авторизации. POST-версия.
        /// </summary>
        /// <param name="user">Авторизируемый пользователь.</param>
        /// <returns>Если авторизация пройдена, то возвращает страницу с неподтвержденными объектами.
        /// Если не пройдена, то вызывается GET-версия метода и в неё передаётся сообщение об ошибке.</returns>
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
