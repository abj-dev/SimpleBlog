namespace SimpleBlog.NHibernate.Entities
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string  Username { get; set; }
        public virtual string Email { get; set; }
        public virtual string PasswordHash { get; set; }
    }
}