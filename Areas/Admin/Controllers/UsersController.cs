﻿using System.Linq;
using System.Web.Mvc;

using NHibernate.Linq;

using SimpleBlog.Areas.Admin.ViewModels;
using SimpleBlog.Infrastructure;
using SimpleBlog.NHibernate;
using SimpleBlog.NHibernate.Entities;
using SimpleBlog.Helpers;

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
                Users = Database.Session.Query<User>().ToArray()
            });
        }

        public ActionResult New()
        {
            return View(new UsersNew
            {
                Roles = Database.Session.Query<Role>().Select(role => new RoleCheckBox
                {
                    Id = role.Id,
                    IsChecked = false,
                    Name = role.Name
                }).ToArray()
            });
        }

        public ActionResult Edit(int id)
        {
            var userToBeEdited = Database.Session.Load<User>(id);

            if (userToBeEdited == null)
                return HttpNotFound();

            return View(new UsersEdit
            {
                Username = userToBeEdited.Username,
                Email = userToBeEdited.Email,
                Roles = Database.Session.Query<Role>().Select(role => new RoleCheckBox
                {
                    Id = role.Id,
                    IsChecked = userToBeEdited.Roles.Contains(role),
                    Name = role.Name
                }).ToArray()
            });
        }

        public ActionResult ResetPassword(int id)
        {
            var userForPasswordReset = Database.Session.Load<User>(id);

            if (userForPasswordReset == null)
                return HttpNotFound();

            return View(new UserResetPassword
            {
                Username = userForPasswordReset.Username
            });
        }

        // POST: Administrator/Users
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult New(UsersNew newUserFormData)
        {
            var user = new User();

            SyncUserRoles.Sync(newUserFormData.Roles, user.Roles);

            if (Database.Session.Query<User>().Any(u => u.Username == newUserFormData.Username))
                ModelState.AddModelError("Username", "Username must be unique");

            if (!ModelState.IsValid)
                return View(newUserFormData);

            user.Email = newUserFormData.Email;
            user.Username = newUserFormData.Username;
            user.SetPassword(newUserFormData.Password);

            Database.Session.Save(user);

            return RedirectToAction("index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UsersEdit updatedUserFormData)
        {
            var userToBeUpdated = Database.Session.Load<User>(id);

            if (userToBeUpdated == null)
                return HttpNotFound();

            SyncUserRoles.Sync(updatedUserFormData.Roles, userToBeUpdated.Roles);

            if (Database.Session.Query<User>().Any(u => u.Username == updatedUserFormData.Username && u.Id != id))
                ModelState.AddModelError("Username", "Username must be unique");

            if (!ModelState.IsValid)
                return View(updatedUserFormData);

            userToBeUpdated.Username = updatedUserFormData.Username;
            userToBeUpdated.Email = updatedUserFormData.Email;

            Database.Session.Update(userToBeUpdated);

            return RedirectToAction("index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ResetPassword(int id, UserResetPassword userUpdatedPasswordFormData)
        {
            var userForPasswordToBeUpdated = Database.Session.Load<User>(id);

            if (userForPasswordToBeUpdated == null)
                return HttpNotFound();

            if (!ModelState.IsValid)
            {
                userUpdatedPasswordFormData.Username = userForPasswordToBeUpdated.Username;

                return View(userUpdatedPasswordFormData);
            }

            userForPasswordToBeUpdated.SetPassword(userUpdatedPasswordFormData.Password);

            Database.Session.Update(userForPasswordToBeUpdated);

            return RedirectToAction("index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var userToBeDeleted = Database.Session.Load<User>(id);

            if (userToBeDeleted == null)
                return HttpNotFound();

            Database.Session.Delete(userToBeDeleted);

            return RedirectToAction("index");
        }
    }
}