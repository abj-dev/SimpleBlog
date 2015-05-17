using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using SimpleBlog.Areas.Admin.ViewModels;
using SimpleBlog.NHibernate;
using SimpleBlog.NHibernate.Entities;

namespace SimpleBlog.Helpers
{
    public static class SyncUserRoles
    {
        public static void Sync(IList<RoleCheckBox> checkBoxes, IList<Role> userRoles)
        {
            var selectedRoles = new List<Role>();

            foreach (var role in Database.Session.Query<Role>())
            {
                var checkBox = checkBoxes.Single(c => c.Id == role.Id);

                checkBox.Name = role.Name;

                if (checkBox.IsChecked)
                    selectedRoles.Add(role);
            }

            foreach (var roleToAdd in selectedRoles.Where(sr => !userRoles.Contains(sr)))
            {
                userRoles.Add(roleToAdd);
            }

            foreach (var roleToRemove in userRoles.Where(cr => !selectedRoles.Contains(cr)).ToList())
            {
                userRoles.Remove(roleToRemove);
            }
        }
    }
}