﻿using System.Web;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using SimpleBlog.NHibernate.Mappings;

namespace SimpleBlog.NHibernate
{
    public static class Database
    {
        // Constants
        // ReSharper disable once InconsistentNaming
        private const string SESSION_KEY = "SimpleBlog.Database.SessionKey";

        // Fields
        private static ISessionFactory _sessionFactory;

        // Properties
        public static ISession NHibernateSession
        {
            get
            {
                return (ISession)HttpContext.Current.Items[SESSION_KEY];
            }
        }

        public static void Configure()
        {
            var nHConfiguration = new Configuration();

            // configure the connection string
            nHConfiguration.Configure();

            // add our mappings
            var modelMapper = new ModelMapper();

            modelMapper.AddMapping<UserMap>();

            nHConfiguration.AddMapping(modelMapper.CompileMappingForAllExplicitlyAddedEntities());

            // create session factory
            _sessionFactory = nHConfiguration.BuildSessionFactory();
        }

        public static void OpenSession()
        {
            HttpContext.Current.Items[SESSION_KEY] = _sessionFactory.OpenSession();
        }

        public static void CloseSession()
        {
            var nHSession = HttpContext.Current.Items[SESSION_KEY] as ISession;

            if (nHSession != null)
                nHSession.Close();

            HttpContext.Current.Items.Remove(SESSION_KEY);
        }
    }
}