using System.Linq;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using NHibernate.Linq;
using SimpleBlog.Areas.Admin.ViewModels;
using SimpleBlog.Infrastructure;
using SimpleBlog.NHibernate;
using SimpleBlog.NHibernate.Entities;

namespace SimpleBlog.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [SelectedTab("Users")]
    public class UsersController : Controller
    {
        // GET: Administrator/Users
        public ActionResult Index()
        {
            return View(new UsersIndex
            {
                Users = Database.NHibernateSession.Query<User>().ToList()
            });
        }

        public ActionResult New()
        {
            return View(new UsersNew
            {

            });
        }

        public ActionResult Edit(int id)
        {
            var userToBeEdited = Database.NHibernateSession.Load<User>(id);

            if (userToBeEdited == null)
                return HttpNotFound();

            return View(new UsersEdit
            {
                Username = userToBeEdited.Username,
                Email = userToBeEdited.Email
            });
        }

        public ActionResult ResetPassword(int id)
        {
            var userForPasswordReset = Database.NHibernateSession.Load<User>(id);

            if (userForPasswordReset == null)
                return HttpNotFound();

            return View(new UserResetPassword
            {
                Username = userForPasswordReset.Username
            });
        }

        // POST: Administrator/Users
        [HttpPost]
        public ActionResult New(UsersNew newUserFormData)
        {
            if (Database.NHibernateSession.Query<User>().Any(u => u.Username == newUserFormData.Username))
                ModelState.AddModelError("Username", "Username must be unique");

            if (!ModelState.IsValid)
                return View(newUserFormData);

            var user = new User
            {
                Email = newUserFormData.Email,
                Username = newUserFormData.Username
            };

            user.SetPassword(newUserFormData.Password);

            Database.NHibernateSession.Save(user);

            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult Edit(int id, UsersEdit updatedUserFormData)
        {
            var userToBeUpdated = Database.NHibernateSession.Load<User>(id);

            if (userToBeUpdated == null)
                return HttpNotFound();

            if (Database.NHibernateSession.Query<User>().Any(u => u.Username == updatedUserFormData.Username && u.Id != id))
                ModelState.AddModelError("Username", "Username must be unique");

            if (!ModelState.IsValid)
                return View(updatedUserFormData);

            userToBeUpdated.Username = updatedUserFormData.Username;
            userToBeUpdated.Email = updatedUserFormData.Email;

            Database.NHibernateSession.Update(userToBeUpdated);

            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult ResetPassword(int id, UserResetPassword userUpdatedPasswordFormData)
        {
            var userForPasswordToBeUpdated = Database.NHibernateSession.Load<User>(id);

            if (userForPasswordToBeUpdated == null)
                return HttpNotFound();

            if (!ModelState.IsValid)
            {
                userUpdatedPasswordFormData.Username = userForPasswordToBeUpdated.Username;

                return View(userUpdatedPasswordFormData);
            }

            userForPasswordToBeUpdated.SetPassword(userUpdatedPasswordFormData.Password);

            Database.NHibernateSession.Update(userForPasswordToBeUpdated);

            return RedirectToAction("index");
        }

        public ActionResult Delete(int id)
        {
            var userToBeDeleted = Database.NHibernateSession.Load<User>(id);

            if (userToBeDeleted == null)
                return HttpNotFound();

            Database.NHibernateSession.Delete(userToBeDeleted);

            return RedirectToAction("index");
        }
    }
}