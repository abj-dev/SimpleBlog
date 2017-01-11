using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using SimpleBlog.Infrastructure.Constants;
using SimpleBlog.NHibernate.Mappings;
using System.Web;

namespace SimpleBlog.NHibernate
{
    public static class Database
    {
        // Fields
        private static ISessionFactory _sessionFactory;

        // Properties
        public static ISession Session
        {
            get
            {
                return (ISession)HttpContext.Current.Items[SiteConstants.SessionKey];
            }
        }

        public static void Configure()
        {
            var nHConfiguration = new Configuration();

            //-> configure the connection string from nhibernate section in web.config.
            nHConfiguration.Configure();

            // add our mappings
            var modelMapper = new ModelMapper();

            //modelMapper.AddMapping<UserMap>();
            //modelMapper.AddMapping<RoleMap>();
            //modelMapper.AddMapping<PostMap>();
            //modelMapper.AddMapping<TagMap>();

            modelMapper.AddMappings(typeof(UserMap).Assembly.GetTypes());

            nHConfiguration.DataBaseIntegration(dbIntegration =>
            {
                dbIntegration.LogFormattedSql = true;
                dbIntegration.LogSqlInConsole = true;
            });

            nHConfiguration.AddMapping(modelMapper.CompileMappingForAllExplicitlyAddedEntities());

            //-> create session factory.
            _sessionFactory = nHConfiguration.BuildSessionFactory();
        }

        public static void OpenSession()
        {
            HttpContext.Current.Items[SiteConstants.SessionKey] = _sessionFactory.OpenSession();
        }

        public static void CloseSession()
        {
            var nHSession = HttpContext.Current.Items[SiteConstants.SessionKey] as ISession;

            nHSession?.Close();

            HttpContext.Current.Items.Remove(SiteConstants.SessionKey);
        }
    }
}