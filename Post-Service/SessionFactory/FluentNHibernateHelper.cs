using Posting_Service.Models;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Configuration;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Posting_Service.Mapping;

namespace Posting_Service.SessionFactory
{
    public class FluentNHibernateHelper
    {
        private readonly string connectionString;
        public FluentNHibernateHelper(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("Default");
        }
        public ISession OpenSession()
        {
            ISessionFactory sessionFactory = Fluently.Configure()
                .Database(MySQLConfiguration.Standard
                  .ConnectionString(connectionString)
                )
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<PostMap>())
                .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(false, false))
                .BuildSessionFactory();

            return sessionFactory.OpenSession();

        }
    }
}
