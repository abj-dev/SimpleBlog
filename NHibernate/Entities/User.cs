namespace SimpleBlog.NHibernate.Entities
{
    public class User
    {
        // Constants
        private const int WORK_FACTOR = 10;

        // Properties
        public virtual int Id { get; set; }
        public virtual string  Username { get; set; }
        public virtual string Email { get; set; }
        public virtual string PasswordHash { get; set; }

        // Methods
        public virtual void SetPassword(string password)
        {
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, WORK_FACTOR);
        }

        public virtual bool CheckPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
        }

        public static void InitFakeHash()
        {
            BCrypt.Net.BCrypt.HashPassword(string.Empty, WORK_FACTOR);
        }
    }
}