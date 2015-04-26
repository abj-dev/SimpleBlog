namespace SimpleBlog.Constants
{
    public static class SiteConstants
    {
        // Constants

        // ReSharper disable once InconsistentNaming
        private const string USER_KEY = "SimpleBlog.Authentication.UserKey";

        // ReSharper disable once InconsistentNaming
        private const string SESSION_KEY = "SimpleBlog.Database.SessionKey";

        public static string UserKey
        {
            get { return USER_KEY; }
        }

        public static string SessionKey
        {
            get { return SESSION_KEY; }
        }
    }
}