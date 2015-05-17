using System.Collections;
using SimpleBlog.Infrastructure.Constants;
using System.Collections.Generic;

namespace SimpleBlog.NHibernate.Entities
{
    public class User
    {
        // Constructors
        public User()
        {
            Roles = new List<Role>();
        }

        // Properties
        public virtual int Id { get; set; }
        public virtual string  Username { get; set; }
        public virtual string Email { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual IList<Role> Roles { get; set; }

        // Methods
        public virtual void SetPassword(string password)
        {
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, SiteConstants.WorkFactor);
        }

        public virtual bool CheckPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
        }

        public static void InitFakeHash()
        {
            BCrypt.Net.BCrypt.HashPassword(string.Empty, SiteConstants.WorkFactor);
        }
    }
}