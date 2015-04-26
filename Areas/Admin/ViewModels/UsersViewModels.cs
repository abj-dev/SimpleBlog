using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SimpleBlog.NHibernate.Entities;

namespace SimpleBlog.Areas.Admin.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class RoleCheckBox
    {
        public int Id { get; set; }
        public bool IsChecked { get; set; }
        public string Name { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UsersIndex
    {
        public IEnumerable<User> Users { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UsersNew
    {
        public IList<RoleCheckBox> Roles { get; set; }

        [Required, MaxLength(128)]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, MaxLength(256), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UsersEdit
    {
        public IList<RoleCheckBox> Roles { get; set; }

        [Required, MaxLength(128)]
        public string Username { get; set; }

        [Required, MaxLength(256), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserResetPassword
    {
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}