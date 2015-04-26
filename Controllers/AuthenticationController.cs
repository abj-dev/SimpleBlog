using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SimpleBlog.ViewModels;

namespace SimpleBlog.Controllers
{
    public class AuthenticationController : Controller
    {
        [HttpGet]
        // GET: Authentication
        public ActionResult Login()
        {
            //return Content("Logged In!");

            return View(new LoginViewModel { });
        }

        // POST: Authentication
        [HttpPost]
        public ActionResult Login(LoginViewModel logindata, string ReturnUrl)
        {
            if (!ModelState.IsValid)
                return View(logindata);

            FormsAuthentication.SetAuthCookie(logindata.UserName, true);

            if (!string.IsNullOrWhiteSpace(ReturnUrl))
                return Redirect(ReturnUrl);
            else
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