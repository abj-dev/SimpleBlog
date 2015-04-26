using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using NHibernate.Linq;
using SimpleBlog.NHibernate;
using SimpleBlog.ViewModels;
using NHibernateEntities = SimpleBlog.NHibernate.Entities;

namespace SimpleBlog.Controllers
{
    public class AuthenticationController : Controller
    {
        [HttpGet]
        // GET: Authentication
        public ActionResult Login()
        {
            return View(new LoginViewModel
            {
                
            });
        }

        // POST: Authentication
        [HttpPost]
        public ActionResult Login(LoginViewModel logindata, string returnUrl)
        {
            var userToBeAuthenticated = Database.NHibernateSession.Query<NHibernateEntities.User>().FirstOrDefault(u => u.Username == logindata.Username);

            if (userToBeAuthenticated != null)
            {
                if (!userToBeAuthenticated.CheckPassword(logindata.Password ?? string.Empty))
                    ModelState.AddModelError("Username", "Username or Password is incorrect");
            }
            else
                NHibernateEntities.User.InitFakeHash();

            if (!ModelState.IsValid)
                return View(logindata);

            // ReSharper disable once PossibleNullReferenceException
            FormsAuthentication.SetAuthCookie(userName: userToBeAuthenticated.Username, createPersistentCookie: true);

            if (!string.IsNullOrWhiteSpace(returnUrl))
                return Redirect(returnUrl);

            return RedirectToRoute("Home");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToRoute("Home");
        }
    }
}