using System.Web;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using SimpleBlog.Infrastructure.Constants;
using SimpleBlog.NHibernate.Mappings;

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

            // configure the connection string
            nHConfiguration.Configure();

            // add our mappings
            var modelMapper = new ModelMapper();

            //modelMapper.AddMapping<UserMap>();
            //modelMapper.AddMapping<RoleMap>();
            //modelMapper.AddMapping<PostMap>();
            //modelMapper.AddMapping<TagMap>();

            modelMapper.AddMappings(typeof(UserMap).Assembly.GetTypes());

            nHConfiguration.AddMapping(modelMapper.CompileMappingForAllExplicitlyAddedEntities());

            // create session factory
            _sessionFactory = nHConfiguration.BuildSessionFactory();
        }

        public static void OpenSession()
        {
            HttpContext.Current.Items[SiteConstants.SessionKey] = _sessionFactory.OpenSession();
        }

        public static void CloseSession()
        {
            var nHSession = HttpContext.Current.Items[SiteConstants.SessionKey] as ISession;

            if (nHSession != null)
                nHSession.Close();

            HttpContext.Current.Items.Remove(SiteConstants.SessionKey);
        }
    }
}